var opts = {
  lines: 10 // The number of lines to draw
, length: 25 // The length of each line
, width: 10 // The line thickness
, radius: 25 // The radius of the inner circle
, scale: 0.25 // Scales overall size of the spinner
, corners: 1 // Corner roundness (0..1)
, color: '#000' // #rgb or #rrggbb or array of colors
, opacity: 0.25 // Opacity of the lines
, rotate: 0 // The rotation offset
, direction: 1 // 1: clockwise, -1: counterclockwise
, speed: 1 // Rounds per second
, trail: 60 // Afterglow percentage
, fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
, zIndex: 2e9 // The z-index (defaults to 2000000000)
, className: 'spinner' // The CSS class to assign to the spinner
, top: '50%' // Top position relative to parent
, left: '50%' // Left position relative to parent
, shadow: false // Whether to render a shadow
, hwaccel: false // Whether to use hardware acceleration
, position: 'relative' // Element positioning
};

var $spinner = document.getElementById('spinner'),
    spinner = new Spinner(opts).spin($spinner);

var padding = 10, // padding needed after migration to d3.v4
    secondsInOneDay = 86400;

var margin = {top: 0, right: 1, bottom: 20, left: 40},
    width = 900 - margin.left - margin.right,
    height = 800 - margin.top - margin.bottom;

var x = d3.scaleLinear().domain([0, 31536000 + secondsInOneDay]).range([1, width]),
    y = d3.scaleLinear().domain([1979, 2019]).range([height - padding, padding]),
    color = d3.scaleOrdinal().range([
              '#f0ff93',
              '#c3f880',
              '#8ef26d',
              '#5bec65',
              '#4ae685',
              '#39dfac',
              '#2ad9ad',
              '#1b99d3',
              '#0d54cd',
              '#000bc7'
            ]);

init();

function init() {
  d3.json(ASSETS_URL + '/json/weekend-boxoffice.json').then(function(obj) {
    var data = obj.data;
    draw(data);
    
    spinner.stop();
    $('#spinner').hide();
    $('.graph').removeClass('hide');
    $('.legend').removeClass('hide');
  })
  .catch(function(error) {
    console.error(error);
  });
} 

function formatRevenue(revenue) {
  if (revenue < 100000) {
    return d3.format('.3s')(revenue)
  }
  if (revenue < 1000000) {
    return d3.format('.5s')(revenue)
  }
  if (revenue < 10000000) {
    return d3.format('.3s')(revenue)
  }
  if (revenue < 100000000) {
    return d3.format('.4s')(revenue)
  }
  if (revenue < 1000000000) {
    return d3.format('.5s')(revenue)
  }
}

function draw(data) {
  var seconds = [0, 
                 2678400, 
                 5184000,
                 7862400,
                 10454400,
                 13132800,
                 15724800,
                 18403200,
                 21081600,
                 23673600,
                 26352000,
                 28944000,
                 31622400
                ],
      months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

  var xAxis = d3.axisBottom(x)
                .tickValues(seconds)
                .tickFormat(function(d, i) { return months[i]; });
               
  var yAxis = d3.axisLeft(y)
                .ticks(40)
                .tickFormat(d3.format(''));

  var svgGraph = d3.select('.graph')
                   .append('svg')
                   .attr('width', width + margin.left + margin.right)
                   .attr('height', height + margin.top + margin.bottom)
                   .append('g')
                   .attr('transform', 'translate(' + margin.left + ',0)');

  // x axis
  svgGraph.append('g')
          .attr('class', 'x axis')
          .attr('transform', 'translate(0,' + height + ')')
          .call(xAxis);

  // y axis
  svgGraph.append('g')
          .attr('class', 'y axis')
          .call(yAxis);

  var tip = d3.tip()
              .attr('class', 'd3-tip')
              .offset([-10, 0])
              .html(function(weekend) {
                var from = moment(weekend.from),
                    to = moment(weekend.to),
                    text = '<p class="movie-title">' + weekend.movie;

                text += ' <span style="color:#ccc">collected</span> $' + formatRevenue(weekend.revenue);
                text += '</p><span style="color:#ccc">over the weekend from</span> ' + from.format('MMMM D, YYYY');
                text += ' <span style="color:#ccc">to</span> ' + to.format('MMMM D, YYYY');

                return text;
              });

  svgGraph.call(tip);

  var boxOffice = svgGraph.selectAll('.year-boxoffice')
                          .data(data)
                          .enter()
                          .append('g')
                          .attr('class', 'year')
                          .attr('transform', function(yearBoxOffice) { return 'translate(0,' + (y(yearBoxOffice.year) - padding) + ')'; });

  boxOffice.selectAll('.weekend-boxoffice')
           .data(function(yearBoxOffice) {
              yearBoxOffice.weekendBoxOffices.forEach(function(weekend) {
                weekend.year = yearBoxOffice.year;
              });
              return yearBoxOffice.weekendBoxOffices;
            })
           .enter()
           .append('rect')
           .attr('fill', function(weekend) {
             if (weekend.revenue < 1000000) {
               return color(1);   
             }
             else if (weekend.revenue < 5000000) {
               return color(2);
             }
             else if (weekend.revenue < 10000000) {
               return color(3);
             }
             else if (weekend.revenue < 25000000) {
               return color(4);
             }
             else if (weekend.revenue < 50000000) {
               return color(5);
             }  
             else if (weekend.revenue < 75000000) {
               return color(6);
             }  
             else if (weekend.revenue < 100000000) {
               return color(7);
             } 
             else if (weekend.revenue < 150000000) {
               return color(8);
             }  
             else if (weekend.revenue < 200000000) {
               return color(9);
             }  
             else {
               return color(10);
             }
           })
           .attr('y', 0)
           .attr('height', 20)
           .attr('x', function(weekend) { 
             var begin = moment(weekend.from),
                 beginingOfYear = moment({ year: weekend.year, month: 0, day: 1 });

             // correction for weekends that span over a year change 
             if (begin.year() < weekend.year) {
              begin = moment({ year: weekend.year, month: 0, day: 1 });
             }
              
             // correction for non-leap years
             var needsCorrection = false;
             if (!moment([weekend.year]).isLeapYear()) {
               var mar1 = moment({ year: weekend.year, month: 2, day: 1, hour: 0, minute: 0, second: 0 });
               needsCorrection = begin.isSameOrAfter(mar1);
             }

             var secondsSinceBeginingOfYear = begin.diff(beginingOfYear, 'seconds');
             return x(secondsSinceBeginingOfYear + (needsCorrection ? secondsInOneDay : 0)); 
           })
           .attr('width', function(weekend) { 
             var begin = moment(weekend.from),
                 end = moment(weekend.to);

             // correction for weekends that span over a year change 
             if (begin.year() < weekend.year) {
              begin = moment({ year: weekend.year, month: 0, day: 1 });
             }
             if (end.year() > weekend.year) {
              end = moment({ year: weekend.year, month: 11, day: 31, hour: 23, minute: 59, second: 59 });
             }

             // correction for non-leap years
             var needsCorrection = false;
             if (!moment([weekend.year]).isLeapYear()) {
               var feb28 = moment({ year: weekend.year, month: 1, day: 28, hour: 0, minute: 0, second: 0 });
               needsCorrection = (begin.isSameOrBefore(feb28) && end.isSameOrAfter(feb28));
             }

             var secondsInTheWeekend = end.diff(begin, 'seconds');
             return x(secondsInTheWeekend + (needsCorrection ? secondsInOneDay : 0)); 
           })
           .on('mouseover', function(weekend) {
             tip.show(weekend);
           })
           .on('mouseout', function(weekend) {
             tip.hide(weekend);
           });

  boxOffice.selectAll('.between-boxoffice')
           .data(function(yearBoxOffice) { 
             yearBoxOffice.betweenBoxOffices.forEach(function(between) {
              between.year = yearBoxOffice.year;
             });
             return yearBoxOffice.betweenBoxOffices; 
           })
           .enter()
           .append('rect')
           .attr('fill', '#eee')
           .attr('y', 0)
           .attr('height', 20)
           .attr('x', function(between) { 
             var begin = moment(between.from),
                 beginingOfYear = moment({ year: begin.year(), month: 0, day: 1 });

             console.log(between.year);
              // correction for non-leap years
             var needsCorrection = false;
             if (!moment([between.year]).isLeapYear()) {
               var mar1 = moment({ year: between.year, month: 2, day: 1, hour: 0, minute: 0, second: 0 });
               needsCorrection = begin.isSameOrAfter(mar1);
               console.log(mar1);
               console.log(begin);
               console.log(needsCorrection);
             }

             var secondsSinceBeginingOfYear = begin.diff(beginingOfYear, 'seconds');
             return x(secondsSinceBeginingOfYear + (needsCorrection ? secondsInOneDay : 0)); 
           })
           .attr('width', function(between) { 
             var begin = moment(between.from),
                 end = moment(between.to),
                 secondsInBetween = end.diff(begin, 'seconds');

             // correction for non-leap years
             var needsCorrection = false;
             if (!moment([between.year]).isLeapYear()) {
               var feb28 = moment({ year: between.year, month: 1, day: 28, hour: 0, minute: 0, second: 0 });
               needsCorrection = (begin.isSameOrBefore(feb28) && end.isSameOrAfter(feb28));
             }

             return x(secondsInBetween + (needsCorrection ? secondsInOneDay : 0)); 
           });

  var svgLegend = d3.select(".legend")
                      .append("svg")
                      .attr("width", "100%")
                      .attr("height", 300);

  var legend = svgLegend.selectAll('.legend')
                        .data(color.domain().slice())
                        .enter()
                        .append('g')
                        .attr('class', 'legend')
                        .attr('transform', function(d, i) { return 'translate(0,' + ((color.domain().length - 1) * 20 - i * 20) + ')'; });

  legend.append('rect')
        .attr('x', 0)
        .attr('width', 18)
        .attr('height', 18)
        .style('fill', color);

  legend.append('text')
        .attr('x', 25)
        .attr('y', 9)
        .attr('dy', '.35em')
        .style('text-anchor', 'begin')
        .text(function(d) { 
          switch (d) {
            case 1: return 'under $1 million';
            case 2: return '$1 to $5 million';
            case 3: return '$5 to $10 million';
            case 4: return '$10 to $25 million';
            case 5: return '$25 to $50 million';
            case 6: return '$50 to $75 million';
            case 7: return '$75 to $100 million';
            case 8: return '$100 to $150 million';
            case 9: return '$150 to $200 million';
            default: return 'over $200 million';
          }
        });
}