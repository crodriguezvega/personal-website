#I "../packages/FSharp.Data.2.4.6/lib/net45"
#I "../packages/Newtonsoft.Json.11.0.2/lib/net45"

#r "FSharp.Data.dll"
#r "Newtonsoft.Json.dll"

#load "OptionConverter.fs"

open System.IO
open FSharp.Data
open Newtonsoft.Json
open Newtonsoft.Json.Converters

fsi.ShowDeclarationValues <- false

type StudioSong = { name: string; songId: int; album: string; albumId: int }
let getStudioSong (name: string) =
  let lcName = name.ToLower()
  match lcName with
  | _ when "I Must Be High".ToLower().Contains(lcName)                                        -> Some({ name = "I Must Be High"; songId = 1; album = "A.M"; albumId = 1 })
  | _ when "Casino Queen".ToLower().Contains(lcName)                                          -> Some({ name = "Casino Queen"; songId = 2; album = "A.M."; albumId = 1 })
  | _ when "Box Full of Letters".ToLower().Contains(lcName)                                   -> Some({ name = "Box Full of Letters"; songId = 3; album = "A.M."; albumId = 1 })
  | _ when "Shouldn't Be Ashamed".ToLower().Contains(lcName)                                  -> Some({ name = "Shouldn't Be Ashamed"; songId = 4; album = "A.M."; albumId = 1 })
  | _ when "Pick Up the Change".ToLower().Contains(lcName)                                    -> Some({ name = "Pick Up the Change"; songId = 5; album = "A.M."; albumId = 1 })
  | _ when "I Thought I Held You".ToLower().Contains(lcName)                                  -> Some({ name = "I Thought I Held You"; songId = 6; album = "A.M."; albumId = 1 })
  | _ when "That's Not the Issue".ToLower().Contains(lcName)                                  -> Some({ name = "That's Not the Issue"; songId = 7; album = "A.M."; albumId = 1 })
  | _ when "It's Just That Simple".ToLower().Contains(lcName)                                 -> Some({ name = "It's Just That Simple"; songId = 8; album = "A.M."; albumId = 1 })
  | _ when "Should've Been in Love".ToLower().Contains(lcName)                                -> Some({ name = "Should've Been in Love"; songId = 9; album = "A.M."; albumId = 1 })
  | _ when "Passenger Side".ToLower().Contains(lcName)                                        -> Some({ name = "Passenger Side"; songId = 10; album = "A.M."; albumId = 1 })
  | _ when "Dash 7".ToLower().Contains(lcName)                                                -> Some({ name = "Dash 7"; songId = 11; album = "A.M."; albumId = 1 })
  | _ when "Blue Eyed Soul".ToLower().Contains(lcName)                                        -> Some({ name = "Blue Eyed Soul"; songId = 12; album = "A.M."; albumId = 1 })
  | _ when "Too Far Apart".ToLower().Contains(lcName)                                         -> Some({ name = "Too Far Apart"; songId = 13; album = "A.M."; albumId = 1 })
  | _ when "Misunderstood".ToLower().Contains(lcName)                                         -> Some({ name = "Misunderstood"; songId = 14; album = "Being There"; albumId = 2 })
  | _ when "Far, Far Away".ToLower().Contains(lcName)                                         -> Some({ name = "Far, Far Away"; songId = 15; album = "Being There"; albumId = 2 })
  | _ when "Monday".ToLower().Contains(lcName)                                                -> Some({ name = "Monday"; songId = 16; album = "Being There"; albumId = 2 })
  | _ when "Outtasite (Outta Mind)".ToLower().Contains(lcName)                                -> Some({ name = "Outtasite (Outta Mind)"; songId = 17; album = "Being There"; albumId = 2 })
  | _ when "Forget the Flowers".ToLower().Contains(lcName)                                    -> Some({ name = "Forget the Flowers"; songId = 18; album = "Being There"; albumId = 2 })
  | _ when "Red-Eyed and Blue".ToLower().Contains(lcName)                                     -> Some({ name = "Red-Eyed and Blue"; songId = 19; album = "Being There"; albumId = 2 })
  | _ when "I Got You (At the End of the Century)".ToLower().Contains(lcName)                 -> Some({ name = "I Got You (At the End of the Century)"; songId = 20; album = "Being There"; albumId = 2 })
  | _ when "What's the World Got in Store".ToLower().Contains(lcName)                         -> Some({ name = "What's the World Got in Store"; songId = 21; album = "Being There"; albumId = 2 })
  | _ when "Hotel Arizona".ToLower().Contains(lcName)                                         -> Some({ name = "Hotel Arizona"; songId = 22; album = "Being There"; albumId = 2 })
  | _ when "Say You Miss Me".ToLower().Contains(lcName)                                       -> Some({ name = "Say You Miss Me"; songId = 23; album = "Being There"; albumId = 2 })
  | _ when "Sunken Treasure".ToLower().Contains(lcName)                                       -> Some({ name = "Sunken Treasure"; songId = 24; album = "Being There"; albumId = 2 })
  | _ when "Someday Soon".ToLower().Contains(lcName)                                          -> Some({ name = "Someday Soon"; songId = 25; album = "Being There"; albumId = 2 })
  | _ when "Outta Mind (Outta Sight)".ToLower().Contains(lcName)                              -> Some({ name = "Outta Mind (Outta Sight)"; songId = 26; album = "Being There"; albumId = 2 })
  | _ when "Someone Else's Song".ToLower().Contains(lcName)                                   -> Some({ name = "Someone Else's Song"; songId = 27; album = "Being There"; albumId = 2 })
  | _ when "Kingpin".ToLower().Contains(lcName)                                               -> Some({ name = "Kingpin"; songId = 28; album = "Being There"; albumId = 2 })
  | _ when "(Was I) In Your Dreams".ToLower().Contains(lcName)                                -> Some({ name = "(Was I) In Your Dreams"; songId = 29; album = "Being There"; albumId = 2 })
  | _ when "Why Would You Wanna Live".ToLower().Contains(lcName)                              -> Some({ name = "Why Would You Wanna Live"; songId = 30; album = "Being There"; albumId = 2 })
  | _ when "The Lonely 1".ToLower().Contains(lcName)                                          -> Some({ name = "The Lonely 1"; songId = 31; album = "Being There"; albumId = 2 })
  | _ when "Dreamer in My Dreams".ToLower().Contains(lcName)                                  -> Some({ name = "Dreamer in My Dreams"; songId = 32; album = "Being There"; albumId = 2 })
  | _ when "Can't Stand It".ToLower().Contains(lcName)                                        -> Some({ name = "Can't Stand It"; songId = 33; album = "Summerteeth"; albumId = 3 })
  | _ when "She's a Jar".ToLower().Contains(lcName)                                           -> Some({ name = "She's a Jar"; songId = 34; album = "Summerteeth"; albumId = 3 })
  | _ when "A Shot in the Arm".ToLower().Contains(lcName)                                     -> Some({ name = "A Shot in the Arm"; songId = 35; album = "Summerteeth"; albumId = 3 })
  | _ when "We're Just Friends".ToLower().Contains(lcName)                                    -> Some({ name = "We're Just Friends"; songId = 36; album = "Summerteeth"; albumId = 3 })
  | _ when "I'm Always in Love".ToLower().Contains(lcName)                                    -> Some({ name = "I'm Always in Love"; songId = 37; album = "Summerteeth"; albumId = 3 })
  | _ when "Nothing'severgonnastandinmyway(again)".ToLower().Contains(lcName)                 -> Some({ name = "Nothing'severgonnastandinmyway(again)"; songId = 38; album = "Summerteeth"; albumId = 3 })
  | _ when "Pieholden Suite".ToLower().Contains(lcName)                                       -> Some({ name = "Pieholden Suite"; songId = 39; album = "Summerteeth"; albumId = 3 })
  | _ when "How to Fight Loneliness".ToLower().Contains(lcName)                               -> Some({ name = "How to Fight Loneliness"; songId = 40; album = "Summerteeth"; albumId = 3 })
  | _ when "Via Chicago".ToLower().Contains(lcName)                                           -> Some({ name = "Via Chicago"; songId = 41; album = "Summerteeth"; albumId = 3 })
  | _ when "ELT".ToLower().Contains(lcName)                                                   -> Some({ name = "ELT"; songId = 42; album = "Summerteeth"; albumId = 3 })
  | _ when "My Darling".ToLower().Contains(lcName)                                            -> Some({ name = "My Darling"; songId = 43; album = "Summerteeth"; albumId = 3 })
  | _ when "When You Wake Up Feeling Old".ToLower().Contains(lcName)                          -> Some({ name = "When You Wake Up Feeling Old"; songId = 44; album = "Summerteeth"; albumId = 3 })
  | _ when "Summer Teeth".ToLower().Contains(lcName)                                          -> Some({ name = "Summer Teeth"; songId = 45; album = "Summerteeth"; albumId = 3 })
  | _ when "In a Future Age".ToLower().Contains(lcName)                                       -> Some({ name = "In a Future Age"; songId = 46; album = "Summerteeth"; albumId = 3 })
  | _ when "I Am Trying to Break Your Heart".ToLower().Contains(lcName)                       -> Some({ name = "I Am Trying to Break Your Heart"; songId = 47; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Kamera".ToLower().Contains(lcName)                                                -> Some({ name = "Kamera"; songId = 48; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Radio Cure".ToLower().Contains(lcName)                                            -> Some({ name = "Radio Cure"; songId = 49; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "War on War".ToLower().Contains(lcName)                                            -> Some({ name = "War on War"; songId = 50; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Jesus, Etc.".ToLower().Contains(lcName)                                           -> Some({ name = "Jesus, Etc."; songId = 51; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Ashes of American Flags".ToLower().Contains(lcName)                               -> Some({ name = "Ashes of American Flags"; songId = 52; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Heavy Metal Drummer".ToLower().Contains(lcName)                                   -> Some({ name = "Heavy Metal Drummer"; songId = 53; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "I'm the Man Who Loves You".ToLower().Contains(lcName)                             -> Some({ name = "I'm the Man Who Loves You"; songId = 54; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Pot Kettle Black".ToLower().Contains(lcName)                                      -> Some({ name = "Pot Kettle Black"; songId = 55; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Poor Places".ToLower().Contains(lcName)                                           -> Some({ name = "Poor Places"; songId = 56; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "Reservations".ToLower().Contains(lcName)                                          -> Some({ name = "Reservations"; songId = 57; album = "Yankee Hotel Foxtrot"; albumId = 4 })
  | _ when "At Least That's What You Said".ToLower().Contains(lcName)                         -> Some({ name = "At Least That's What You Said"; songId = 58; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Hell Is Chrome".ToLower().Contains(lcName)                                        -> Some({ name = "Hell Is Chrome"; songId = 59; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Spiders (Kidsmoke)".ToLower().Contains(lcName)                                    -> Some({ name = "Spiders (Kidsmoke)"; songId = 60; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Muzzle of Bees".ToLower().Contains(lcName)                                        -> Some({ name = "Muzzle of Bees"; songId = 61; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Hummingbird".ToLower().Contains(lcName)                                           -> Some({ name = "Hummingbird"; songId = 62; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Handshake Drugs".ToLower().Contains(lcName)                                       -> Some({ name = "Handshake Drugs"; songId = 63; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Wishful Thinking".ToLower().Contains(lcName)                                      -> Some({ name = "Wishful Thinking"; songId = 64; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Company in My Back".ToLower().Contains(lcName)                                    -> Some({ name = "Company in My Back"; songId = 65; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "I'm a Wheel".ToLower().Contains(lcName)                                           -> Some({ name = "I'm a Wheel"; songId = 66; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Theologians".ToLower().Contains(lcName)                                           -> Some({ name = "Theologians"; songId = 67; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Less Than You Think".ToLower().Contains(lcName)                                   -> Some({ name = "Less Than You Think"; songId = 68; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "The Late Greats".ToLower().Contains(lcName)                                       -> Some({ name = "The Late Greats"; songId = 69; album = "A Ghost Is Born"; albumId = 5 })
  | _ when "Either Way".ToLower().Contains(lcName)                                            -> Some({ name = "Either Way"; songId = 70; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "You Are My Face".ToLower().Contains(lcName)                                       -> Some({ name = "You Are My Face"; songId = 71; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Impossible Germany".ToLower().Contains(lcName)                                    -> Some({ name = "Impossible Germany"; songId = 72; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Sky Blue Sky".ToLower().Contains(lcName)                                          -> Some({ name = "Sky Blue Sky"; songId = 73; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Side With the Seeds".ToLower().Contains(lcName)                                   -> Some({ name = "Side With the Seeds"; songId = 74; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Shake It Off".ToLower().Contains(lcName)                                          -> Some({ name = "Shake It Off"; songId = 75; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Please Be Patient With Me".ToLower().Contains(lcName)                             -> Some({ name = "Please Be Patient With Me"; songId = 76; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Hate It Here".ToLower().Contains(lcName)                                          -> Some({ name = "Hate It Here"; songId = 77; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Leave Me (Like You Found Me)".ToLower().Contains(lcName)                          -> Some({ name = "Leave Me (Like You Found Me)"; songId = 78; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Walken".ToLower().Contains(lcName)                                                -> Some({ name = "Walken"; songId = 79; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "What Light".ToLower().Contains(lcName)                                            -> Some({ name = "What Light"; songId = 80; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "On and On and On".ToLower().Contains(lcName)                                      -> Some({ name = "On and On and On"; songId = 81; album = "Sky Blue Sky"; albumId = 6 })
  | _ when "Wilco (The Song)".ToLower().Contains(lcName)                                      -> Some({ name = "Wilco (The Song)"; songId = 82; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "Deeper Down".ToLower().Contains(lcName)                                           -> Some({ name = "Deeper Down"; songId = 83; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "One Wing".ToLower().Contains(lcName)                                              -> Some({ name = "One Wing"; songId = 84; album = "Wilco (The Album)"; albumId = 7; })
  | _ when "Bull Black Nova".ToLower().Contains(lcName)                                       -> Some({ name = "Bull Black Nova"; songId = 85; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "You and I".ToLower().Contains(lcName)                                             -> Some({ name = "You and I"; songId = 86; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "You Never Know".ToLower().Contains(lcName)                                        -> Some({ name = "You Never Know"; songId = 87; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "Country Disappeared".ToLower().Contains(lcName)                                   -> Some({ name = "Country Disappeared"; songId = 88; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "Solitaire".ToLower().Contains(lcName)                                             -> Some({ name = "Solitaire"; songId = 89; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "I'll Fight".ToLower().Contains(lcName)                                            -> Some({ name = "I'll Fight"; songId = 90; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "Sonny Feeling".ToLower().Contains(lcName)                                         -> Some({ name = "Sonny Feeling"; songId = 91; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "Everlasting Everything".ToLower().Contains(lcName)                                -> Some({ name = "Everlasting Everything"; songId = 92; album = "Wilco (The Album)"; albumId = 7 })
  | _ when "Art of Almost".ToLower().Contains(lcName)                                         -> Some({ name = "Art of Almost"; songId = 93; album = "The Whole Love"; albumId = 8 })
  | _ when "I Might".ToLower().Contains(lcName)                                               -> Some({ name = "I Might"; songId = 94; album = "The Whole Love"; albumId = 8 })
  | _ when "Sunloathe".ToLower().Contains(lcName)                                             -> Some({ name = "Sunloathe"; songId = 95; album = "The Whole Love"; albumId = 8 })
  | _ when "Dawned on Me".ToLower().Contains(lcName)                                          -> Some({ name = "Dawned on Me"; songId = 96; album = "The Whole Love"; albumId = 8 })
  | _ when "Black Moon".ToLower().Contains(lcName)                                            -> Some({ name = "Black Moon"; songId = 97; album = "The Whole Love"; albumId = 8 })
  | _ when "Born Alone".ToLower().Contains(lcName)                                            -> Some({ name = "Born Alone"; songId = 98; album = "The Whole Love"; albumId = 8 })
  | _ when "Open Mind".ToLower().Contains(lcName)                                             -> Some({ name = "Open Mind"; songId = 99; album = "The Whole Love"; albumId = 8 })
  | _ when "Capitol City".ToLower().Contains(lcName)                                          -> Some({ name = "Capitol City"; songId = 100; album = "The Whole Love"; albumId = 8 })
  | _ when "Standing O".ToLower().Contains(lcName)                                            -> Some({ name = "Standing O"; songId = 101; album = "The Whole Love"; albumId = 8 })
  | _ when "Rising Red Lung".ToLower().Contains(lcName)                                       -> Some({ name = "Rising Red Lung"; songId = 102; album = "The Whole Love"; albumId = 8 })
  | _ when "Whole Love".ToLower().Contains(lcName)                                            -> Some({ name = "Whole Love"; songId = 103; album = "The Whole Love"; albumId = 8 })
  | _ when "One Sunday Morning (Song for Jane Smiley's Boyfriend)".ToLower().Contains(lcName) -> Some({ name = "One Sunday Morning (Song for Jane Smiley's Boyfriend)"; songId = 104; album = "The Whole Love"; albumId = 8 })
  | _ when "EKG".ToLower().Contains(lcName)                                                   -> Some({ name = "EKG"; songId = 105; album = "Star Wars"; albumId = 9 })
  | _ when "More...".ToLower().Contains(lcName)                                               -> Some({ name = "More..."; songId = 106; album = "Star Wars"; albumId = 9 })
  | _ when "Random Name Generator".ToLower().Contains(lcName)                                 -> Some({ name = "Random Name Generator"; songId = 107; album = "Star Wars"; albumId = 9 })
  | _ when "The Joke Explained".ToLower().Contains(lcName)                                    -> Some({ name = "The Joke Explained"; songId = 108; album = "Star Wars"; albumId = 9 })
  | _ when "You Satellite".ToLower().Contains(lcName)                                         -> Some({ name = "You Satellite"; songId = 109; album = "Star Wars"; albumId = 9 })
  | _ when "Taste the Ceiling".ToLower().Contains(lcName)                                     -> Some({ name = "Taste the Ceiling"; songId = 110; album = "Star Wars"; albumId = 9 })
  | _ when "Pickled Ginger".ToLower().Contains(lcName)                                        -> Some({ name = "Pickled Ginger"; songId = 111; album = "Star Wars"; albumId = 9 })
  | _ when "Where Do I Begin".ToLower().Contains(lcName)                                      -> Some({ name = "Where Do I Begin"; songId = 112; album = "Star Wars"; albumId = 9 })
  | _ when "Cold Scope".ToLower().Contains(lcName)                                            -> Some({ name = "Cold Scope"; songId = 113; album = "Star Wars"; albumId = 9 })
  | _ when "King of You".ToLower().Contains(lcName)                                           -> Some({ name = "King of You"; songId = 114; album = "Star Wars"; albumId = 9 })
  | _ when "Magnetized".ToLower().Contains(lcName)                                            -> Some({ name = "Magnetized"; songId = 115; album = "Star Wars"; albumId = 9 })
  | _ when "Normal American Kids".ToLower().Contains(lcName)                                  -> Some({ name = "Normal American Kids"; songId = 116; album = "Schmilco"; albumId = 10 })
  | _ when "If I Ever Was a Child".ToLower().Contains(lcName)                                 -> Some({ name = "If I Ever Was a Child"; songId = 117; album = "Schmilco"; albumId = 10 })
  | _ when "Cry All Day".ToLower().Contains(lcName)                                           -> Some({ name = "Cry All Day"; songId = 118; album = "Schmilco"; albumId = 10 })
  | _ when "Common Sense".ToLower().Contains(lcName)                                          -> Some({ name = "Common Sense"; songId = 119; album = "Schmilco"; albumId = 10 })
  | _ when "Nope".ToLower().Contains(lcName)                                                  -> Some({ name = "Nope"; songId = 120; album = "Schmilco"; albumId = 10 })
  | _ when "Someone to Lose".ToLower().Contains(lcName)                                       -> Some({ name = "Someone to Lose"; songId = 121; album = "Schmilco"; albumId = 10 })
  | _ when "Happiness".ToLower().Contains(lcName)                                             -> Some({ name = "Happiness"; songId = 122; album = "Schmilco"; albumId = 10 })
  | _ when "Quarters".ToLower().Contains(lcName)                                              -> Some({ name = "Quarters"; songId = 123; album = "Schmilco"; albumId = 10 })
  | _ when "Locator".ToLower().Contains(lcName)                                               -> Some({ name = "Locator"; songId = 124; album = "Schmilco"; albumId = 10 })
  | _ when "Shrug and Destroy".ToLower().Contains(lcName)                                     -> Some({ name = "Shrug and Destroy"; songId = 125; album = "Schmilco"; albumId = 10 })
  | _ when "We Aren't the World (Safety Girl)".ToLower().Contains(lcName)                     -> Some({ name = "We Aren't the World (Safety Girl)"; songId = 126; album = "Schmilco"; albumId = 10 })
  | _ when "Just Say Goodbye".ToLower().Contains(lcName)                                      -> Some({ name = "Just Say Goodbye"; songId = 127; album = "Schmilco"; albumId = 10 })
  | _ -> None

type SetlistFm = JsonProvider<"../data/fm-setlists-sample.json">
let doc = SetlistFm.GetSample()
let apiKey = "<api key>"
let baseUrl = "https://api.setlist.fm/rest/1.0/artist/9e53f84d-ef44-4c16-9677-5fd4d78cbd7d/setlists"

let total = float doc.Total
let itemsPerPage = float doc.ItemsPerPage
let pages = int (ceil(total / itemsPerPage))

type Song = { name: string; songId: int option; album: string option; albumId: int option }
type Set = { songs: Song list }
type Venue = { name: string; city: string; state: string option; country: string }
type Setlist = { date: string; venue: Venue; songs: Song list }
type YearlySetlist = { year: int; numberOfShows: int; numberOfCountries: int; shows: Setlist list }
type Data = { data: YearlySetlist list}

let buildSongRecord song =
  match getStudioSong song with
  | Some(studioSong) -> { name = studioSong.name; songId = Some(studioSong.songId); album = Some(studioSong.album); albumId = Some(studioSong.albumId) } : Song
  | None -> { name = song; songId = None; album = None; albumId = None } : Song

let mapStateCode (fmCity: JsonProvider<"../data/fm-setlists-sample.json">.City) = 
  if fmCity.Country.Code = "US" then
    fmCity.StateCode.String
  else
    None

let mapVenue (fmVenue: JsonProvider<"../data/fm-setlists-sample.json">.Venue) =
  let stateCode = mapStateCode fmVenue.City
  { name = fmVenue.Name; city = fmVenue.City.Name; state = stateCode; country = fmVenue.City.Country.Name }

let mapSong (fmSong: JsonProvider<"../data/fm-setlists-sample.json">.Song) =
  buildSongRecord fmSong.Name

let mapSet (fmSet: JsonProvider<"../data/fm-setlists-sample.json">.Set) =
  fmSet.Song |> List.ofArray |> List.map mapSong

let mapSetlist (fmSetlist: JsonProvider<"../data/fm-setlists-sample.json">.Setlist) =
  let date = match fmSetlist.EventDate.String with
             | None -> null
             | Some(str) ->
               let dt = System.DateTime.ParseExact(str, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
               dt.ToString("yyyy-MM-ddThh:mm:ssZ")
  let venue = mapVenue fmSetlist.Venue
  let songs = fmSetlist.Sets.Set |> List.ofArray |> List.collect mapSet
  { date = date; venue = venue; songs = songs }

let rec getSetlists pages =
  match pages with
  | [] -> []
  | head :: tail ->
    printfn "processing page: %d" head

    let queryParameters = ["p", head.ToString()]
    let headers = ["Accept", "application/json"; "x-api-key", apiKey]
    let response = Http.RequestString(baseUrl, httpMethod = "GET", query = queryParameters, headers = headers)

    let bytes = System.Text.Encoding.UTF8.GetBytes response
    use stream = new MemoryStream(bytes)
    let data = SetlistFm.Load(stream)
    let setlists = data.Setlist |> List.ofArray |> List.map mapSetlist
    setlists @ getSetlists tail

let allSetlists = getSetlists [1 .. 68] //68
let allSongs = allSetlists |> List.collect (fun x -> x.songs)
let songsFromStudioAlbums = allSongs |> List.filter (fun x -> Option.isSome x.albumId && Option.isSome x.songId)
let maximumCountSongsInShow = allSetlists |> List.map (fun x -> x.songs.Length)
                                             |> List.max 
printfn "Total number of songs: %d" allSongs.Length    
printfn "Number of songs from studio albums: %d" songsFromStudioAlbums.Length
printfn "Maximum count of songs in one show: %d" maximumCountSongsInShow
                    
let numberOfSetlists = allSetlists.Length

// Number or times each song has been played
let songStatistics = allSongs |> List.sortBy (fun x -> x.songId)
                              |> List.groupBy (fun x -> x.name)
                              |> List.map (fun x -> (fst x, (snd x).Length))
let statisticsLines = "Song statistics:" :: (songStatistics |> List.map (fun x -> sprintf "%s %d" (fst x) (snd x)))
let statisticsPath = Path.Combine(__SOURCE_DIRECTORY__, "..", "data/statistics.txt")
File.WriteAllLines(statisticsPath, statisticsLines)   

let distinctSongs = allSongs |> List.distinctBy (fun x -> x.name)

// Setlists without any songs
let emptySetlists = allSetlists |> List.filter (fun x -> x.songs.IsEmpty)
printfn "Number of empty setlists: %d" emptySetlists.Length

// Songs without an assigned albumId and songId 
let unAssignedSongs = distinctSongs |> List.filter (fun x -> Option.isNone x.albumId && Option.isNone x.songId)
                                    |> List.map (fun x -> x.name)
let unAssignedSongsLines = "Unassigned songs:" :: (unAssignedSongs |> List.map (fun x -> sprintf "%s" x))
let unAssignedSongsPath = Path.Combine(__SOURCE_DIRECTORY__, "..", "data/unassigned-songs.txt")
File.WriteAllLines(unAssignedSongsPath, unAssignedSongsLines)   

let setlists = allSetlists |> List.filter (fun x -> not x.songs.IsEmpty)
printfn "Number of recorded setlists: %d" setlists.Length

let buildYearlySetlist setlistForYear =
  let year = fst setlistForYear
  let shows = snd setlistForYear
  let numberOfShows = shows |> List.length
  let numberOfCountries = shows |> List.map (fun x -> x.venue.country)
                                |> List.distinct
                                |> List.length
  { year = year; numberOfShows = numberOfShows; numberOfCountries = numberOfCountries; shows = shows }

let setlistsGroupedByYear = setlists |> List.groupBy (fun x -> (System.DateTime.Parse x.date).Year)
                                     |> List.map buildYearlySetlist
let data = { data = setlistsGroupedByYear }

// Write to JSON file
let stringWriter = new StringWriter()
let jsonSerializer = new JsonSerializer()
jsonSerializer.Converters.Add(new OptionConverter())
jsonSerializer.Formatting <- Formatting.Indented
jsonSerializer.NullValueHandling <- NullValueHandling.Ignore
jsonSerializer.Serialize(stringWriter, data)

let setlistsPath = Path.Combine(__SOURCE_DIRECTORY__, "..", "data/wilco-setlists.json")
File.WriteAllText(setlistsPath, stringWriter.ToString()) 