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

var padding = 10; // padding needed after migration to d3.v4

var margin = {top: 15, right: 20, bottom: 0, left: 160},
    width = 900 - margin.left - margin.right,
    height = 1200 - margin.top - margin.bottom;   

var maxTeamSalary = 0;

var x = d3.scaleLinear().range([0, width]),
    y = d3.scaleBand().range([0, height - padding], .1),
    color = d3.scaleOrdinal().range(['#ffeeee', '#fed8d9', '#febabc', '#ff989b', '#fe7275', '#ff2e33']);

init();

function init() {
  d3.json(ASSETS_URL + '/json/nba-salaries.json').then(function(obj) {
    var data = obj.data;

    data.forEach(function(team) {
      team.totalSalary = 0;
      team.players.sort(function(a, b) { return a.salary - b.salary; });

      team.players.forEach(function(player) {
        player.x = team.totalSalary;   
        player.team = team.name;
        team.totalSalary += player.salary;     
      });
    });
    
    draw(data);
    
    spinner.stop();
    $('#spinner').hide();
    $('.graph').removeClass('hide');
  })
  .catch(function(error) {
    console.error(error);
  });
}  

function draw(data) {
  x.domain([0, 105000000]);
  y.domain(data.map(function(team) { return team.name; }));

  var xAxis = d3.axisTop(x)
                .tickValues([0, 20000000, 40000000, 60000000, 80000000, 100000000])
                .tickFormat(function(value) { return '$' + d3.format('.0s')(value); });

  var yAxis = d3.axisLeft(y);

  var svg = d3.select('.graph')
              .append('svg')
              .attr('width', width + margin.left + margin.right)
              .attr('height', height + margin.top + margin.bottom)
              .append('g')
              .attr('transform', 'translate(' + margin.left + ',0)');

  var tip = svg.append('g')
               .attr('class', 'tip')
               .style('display', 'none');

  var tipText = tip.append('text');

  svg.append('g')
     .attr('class', 'grid')
     .attr('transform', 'translate(0,' + margin.top + ')')
     .call(xAxis.tickSize(-height))
     .selectAll('.tick')
     .data(x.ticks(), function(d) { return d; })

  svg.append('g')
     .attr('class', 'y axis')
     .attr('transform', 'translate(0,' + (margin.top + padding) + ')')
     .call(yAxis);

  var logos = svg.append('g')
                 .attr('class', 'logos')
                 .attr('transform', 'translate(' + -margin.left + ',0)')
                 .attr('width', 20);

  // http://jsfiddle.net/am8ZB/
  logos.selectAll('.logo')
       .data(data)
       .enter()
       .append('svg:image')
       .attr('transform', function(team) { return 'translate(0,' + (margin.top + y(team.name) + y.bandwidth() / 4 + padding) + ')'; })
       .attr('xlink:href', function(team) {
         return ASSETS_URL + '/img/nba-salaries/' + team.name.toLowerCase().replace(/ /g, '-') + '.png';
       })
       .attr('width', 22)
       .attr('height', 22);

  var team = svg.selectAll('.team')
                .data(data)
                .enter()
                .append('g')
                .attr('class', 'g')
                .attr('transform', function(team) { return 'translate(0,' + (margin.top + y(team.name) + y.step() / 4 + padding) + ')'; });

  team.selectAll('rect')
      .data(function(team) { return team.players; })
      .enter()
      .append('rect')
      .attr('fill', function(player) { 
        if (player.salary < 1000000) {
          return color(1);   
        }
        else if (player.salary < 5000000) {
          return color(2);
        }
        else if (player.salary < 10000000) {
          return color(3);
        }
        else if (player.salary < 15000000) {
          return color(4);
        }
        else if (player.salary < 20000000) {
          return color(5);
        }  
        else {
          return color(6);
        }
      })
      .attr('y', function(player) { return 0; })
      .attr('height', y.step() / 2)
      .attr('x', function(player) { return x(player.x); })
      .attr('width', function(player) { return x(player.salary); });

  team.selectAll('rect')
      .on('mouseover', function(player) {
        var format = d3.format(',.0f');
        tip.style('display', null);

        tipText.text(player.name + ', $' + format(player.salary)); 
        var tipTextWidth = tipText.node().getBBox().width;

        var xPos = x(player.x) + (x(player.salary) / 2) - (tipTextWidth / 2);
        var yPos = y(player.team) + margin.top + 3;

        tip.attr('transform', 'translate(' + xPos + ',' + (yPos + padding) + ')');
        d3.select(this).attr('stroke', '#666').attr('stroke-width', 1);
      })
      .on('mouseout', function(player) {
        tip.style('display', 'none');
        d3.select(this).attr('stroke', null);  
      });

  var legend = svg.selectAll('.legend')
                  .data(color.domain().slice())
                  .enter()
                  .append('g')
                  .attr('class', 'legend')
                  .attr('transform', function(d, i) { return 'translate(-50,' + (200 + i * 20) + ')'; });

  legend.append('rect')
        .attr('x', width - 18)
        .attr('width', 18)
        .attr('height', 18)
        .style('fill', color);

  legend.append('text')
        .attr('x', width - 24)
        .attr('y', 9)
        .attr('dy', '.35em')
        .style('text-anchor', 'end')
        .text(function(d) { 
          switch (d) {
            case 1: return 'under $1 million';
            case 2: return '$1 to $5 million';
            case 3: return '$5 to $10 million';
            case 4: return '$10 to $15 million';
            case 5: return '$15 to $20 million';
            default: return 'over $20 million';
          }
        });
}