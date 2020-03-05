#I "/Users/carlosrodriguez/Documents/Code/Data.Processor/packages/FSharp.Data.3.3.2/lib/net45"
#I "/Users/carlosrodriguez/Documents/Code/Data.Processor/packages/Newtonsoft.Json.12.0.3/lib/net45"

#r "FSharp.Data.dll"
#r "Newtonsoft.Json.dll"

#load "/Users/carlosrodriguez/Documents/Code/Data.Processor/src/OptionConverter.fs"

open System
open System.IO
open System.Text.RegularExpressions
open FSharp.Data
open Newtonsoft.Json
open Newtonsoft.Json.Converters

let sameMonthWeekendRegex = new Regex("^([a-zA-Z]{3})\s+([0-9]{1,2})-([0-9]{1,2})$")
let differentMonthWeekendRegex = new Regex("^([a-zA-Z]{3})\s+([0-9]{1,2})-([a-zA-Z]{3})\s+([0-9]{1,2})$")
let differentYearWeekendRegex = new Regex("^([a-zA-Z]{3})\s+([0-9]{1,2})-([a-zA-Z]{3})\s+([0-9]{1,2}).*([0-9]{4})$")
type BoxOffice = HtmlProvider<"/Users/carlosrodriguez/Documents/Code/Data.Processor/data/boxoffice/weekend-boxoffice.html">

type WeekendBoxOffice = {
  [<JsonProperty(PropertyName = "from")>]
  fromDay: DateTime;
  [<JsonProperty(PropertyName = "to")>]
  toDay: DateTime;
  movie: string; 
  revenue: decimal; 
  distributor: string;
}
type BetweenBoxOffice = {
  [<JsonProperty(PropertyName = "from")>]
  fromDay: DateTime;
  [<JsonProperty(PropertyName = "to")>]
  toDay: DateTime;
}
type Weekend = {
  year: int;
  weekNumber: int;
  includesHoliday: bool;
  fromDay: DateTime option;
  toDay: DateTime option;
}
type YearBoxOffice = {
  year: int;
  weekendBoxOffices: WeekendBoxOffice seq
  betweenBoxOffices: BetweenBoxOffice seq
}
type Data = { data: YearBoxOffice seq}

let toMonthNumber =
  function
  | "Jan" -> 1
  | "Feb" -> 2
  | "Mar" -> 3
  | "Apr" -> 4
  | "May" -> 5
  | "Jun" -> 6
  | "Jul" -> 7
  | "Aug" -> 8
  | "Sep" -> 9
  | "Oct" -> 10
  | "Nov" -> 11
  | _     -> 12

let parseDate (date: string) (year: int): (DateTime * DateTime) option =
  let matchSameMonthWeekend (date: string): (int * int * int * int) option =
    let sameMonthMatch = sameMonthWeekendRegex.Match date
    if (sameMonthMatch.Success) then
      let groups = sameMonthMatch.Groups
      let beginMonth = groups.[1].Value |> toMonthNumber
      let beginDay = groups.[2].Value |> int
      let endMonth = beginMonth
      let endDay = groups.[3].Value |> int
      Some (beginMonth, beginDay, endMonth, endDay)
    else
      None

  let matchDifferentMonthWeekend (date: string): (int * int * int * int) option =
    let differentMonthMatch = differentMonthWeekendRegex.Match date
    if (differentMonthMatch.Success) then
      let groups = differentMonthMatch.Groups
      let beginMonth = groups.[1].Value |> toMonthNumber
      let beginDay = groups.[2].Value |> int
      let endMonth = groups.[3].Value |> toMonthNumber
      let endDay = groups.[4].Value |> int
      Some (beginMonth, beginDay, endMonth, endDay)
    else
      None

  let matchDifferentYearWeekend (date: string): (int * int * int * int * int) option =
    let differentYearMatch = differentYearWeekendRegex.Match date
    if (differentYearMatch.Success) then
      let groups = differentYearMatch.Groups
      let beginMonth = groups.[1].Value |> toMonthNumber
      let beginDay = groups.[2].Value |> int
      let endMonth = groups.[3].Value |> toMonthNumber
      let endDay = groups.[4].Value |> int
      let endYear = groups.[5].Value |> int
      Some (beginMonth, beginDay, endMonth, endDay, endYear)
    else
      None

  match (matchSameMonthWeekend date, matchDifferentMonthWeekend date, matchDifferentYearWeekend date) with
  | (Some (beginMonth, beginDay, endMonth, endDay), _, _)
  | (_, Some (beginMonth, beginDay, endMonth, endDay), _) ->
    let fromDay = new DateTime(year, beginMonth, beginDay, 0, 0, 0)
    let toDay = new DateTime(year, endMonth, endDay, 23, 59, 59)
    Some (fromDay, toDay)
  | (_, _, Some (beginMonth, beginDay, endMonth, endDay, endYear)) ->
    let fromDay = new DateTime(year, beginMonth, beginDay, 0, 0, 0)
    let toDay = new DateTime(endYear, endMonth, endDay, 23, 59, 59)
    Some (fromDay, toDay)
  | _ -> None

let processWeekendRow (year: int) (row: HtmlNode): Weekend =
  let (date, weekNumber) =
    row.CssSelect("td.mojo-field-type-date_interval > a")
    |> List.map(fun x -> x.DirectInnerText().Trim())
    |> List.pairwise
    |> List.head
  let holiday =
    row.CssSelect("td.mojo-field-type-date_interval span")
    |> List.tryHead

  printfn "%s" date
  let dates = parseDate date year
  {
    year = year;
    weekNumber = weekNumber.AsInteger();
    includesHoliday = holiday.IsSome;
    fromDay = dates |> Option.map fst;
    toDay = dates |> Option.map snd;
  }

let parseWeekends (year: int): Weekend seq = 
  printfn "year: %d" year
  let url = sprintf "https://www.boxofficemojo.com/weekend/by-year/%d" year
  let doc = HtmlDocument.Load(url)
  let weekendsTable = doc.CssSelect(".mojo-body-table > tr")
  weekendsTable 
  |> Seq.skip 1
  |> Seq.map (processWeekendRow year)
  |> Seq.filter (fun weekend -> not weekend.includesHoliday)

let fetchBoxOffice (weekend: Weekend): (int * WeekendBoxOffice) list =
  let year = weekend.year 
  let weekNumber = weekend.weekNumber
  printfn "year: %d week number: %d" year weekNumber

  let url = sprintf "https://www.boxofficemojo.com/weekend/%dW%02d" year weekNumber
  let doc = BoxOffice.Load(url)
  let boxOffice =
    doc.Tables.Table1.Rows 
    |> Seq.head 
    |> (fun row -> {
         fromDay = weekend.fromDay.Value;
         toDay = weekend.toDay.Value;
         movie = row.Release;
         revenue = row.Gross;
         distributor = row.Distributor 
       })

  let fromDay = boxOffice.fromDay
  let toDay = boxOffice.toDay
  if fromDay.Year <> toDay.Year then
    [(fromDay.Year, boxOffice); (toDay.Year, boxOffice)]
  else
    [(year, boxOffice)]

let createBetweenBoxOffices (year: int) (boxOffices: WeekendBoxOffice seq): BetweenBoxOffice seq =
  let length = boxOffices |> Seq.length
  seq {
    for i in 0..length - 1 do
      let boxOffice = boxOffices |> Seq.item i
      if i = 0 then  
        let firstJan = new DateTime(year, 1, 1, 0, 0, 0)
        if boxOffice.fromDay > firstJan then
          yield { fromDay = firstJan; toDay = boxOffice.fromDay.AddSeconds(-1.0) }

      if i < length - 1 then
        let nextBoxOffice = boxOffices |> Seq.item (i + 1)
        yield { fromDay = boxOffice.toDay.AddSeconds(1.0); toDay = nextBoxOffice.fromDay.AddSeconds(-1.0) }

      if i = length - 1 then
        let lastDec = new DateTime(year, 12, 31, 23, 59, 59)
        if boxOffice.toDay < lastDec then
          yield { fromDay = boxOffice.toDay.AddSeconds(1.0); toDay = lastDec }
  }

let weekends =
  [1979..2019]
  |> Seq.map parseWeekends
  |> Seq.concat

let yearBoxOffices = 
  weekends
  |> Seq.map fetchBoxOffice
  |> Seq.concat
  |> Seq.groupBy fst
  |> Seq.map (fun (year, boxOffices) -> 
       let weekendBoxOffices =
         boxOffices
         |> Seq.map snd
         |> Seq.sortBy (fun boxOffice -> boxOffice.fromDay)
       let betweenBoxOffices = createBetweenBoxOffices year weekendBoxOffices
       { year = year; weekendBoxOffices = weekendBoxOffices; betweenBoxOffices = betweenBoxOffices }
     )

let data = { data = yearBoxOffices }

// Write to JSON file
let stringWriter = new StringWriter()
let jsonSerializer = new JsonSerializer()
jsonSerializer.Converters.Add(new OptionConverter())
jsonSerializer.Formatting <- Formatting.Indented
jsonSerializer.NullValueHandling <- NullValueHandling.Ignore
jsonSerializer.Serialize(stringWriter, data)

let setlistsPath = Path.Combine(__SOURCE_DIRECTORY__, "..", "data/boxoffice/weekend-boxoffice.json")
File.WriteAllText(setlistsPath, stringWriter.ToString())