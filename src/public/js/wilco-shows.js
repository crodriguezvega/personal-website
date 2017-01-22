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

var $spinner = $('#spinner'),
    spinner = new Spinner(opts).spin($spinner.get(0));

var margin = {top: 0, right: 0, bottom: 0, left: 0},
    width = 500, // 70 + 43 (shows) * 10
    height = 12243; // 23 (years) * 1217 (shows) * 10 + 50 (extra space for stats of 1994)

var heightSong = 10,
    widthSong = 10;

var viewModel = {
  albums: ko.observableArray([]),
  slicedAlbums: ko.observableArray([]),
  options: ko.observableArray([]),
  selected: ko.observable()
};

ko.applyBindings(viewModel);

$.getJSON('/json/wilco-shows/albums.json', function(data) {
  viewModel.options.push({ id: 0, name: 'All', disabled: false });
  viewModel.options.push({ disabled: true });

  var chunk1 = [], chunk2 = [], chunk3 = [], chunk4 = [];
  data.albums.forEach(function(album, index) {
    album.formattedReleased = moment(album.released).format('MMMM D, YYYY');
    viewModel.options.push({ id: album.id, name: album.name, disabled: false });
    viewModel.albums.push(album);

    var aux = { name: album.name, abbrev: album.abbrev, color: album.color };
    if (index < 3) { chunk1.push(aux); }
    else if (index < 6) { chunk2.push(aux); }
    else if (index < 9) { chunk3.push(aux); }
    else { chunk4.push(aux); }
  });
  
  viewModel.slicedAlbums.push(chunk1);
  viewModel.slicedAlbums.push(chunk2);
  viewModel.slicedAlbums.push(chunk3);
  viewModel.slicedAlbums.push(chunk4);

  d3.json('/json/wilco-shows/shows.json', function(error, obj) {
    var data = obj.data;
  
    spinner.stop();
    $spinner.hide();
    $('.graph-section').removeClass('hide');

    draw(data);
    subscribe();

    viewModel.selected(0); // 'All' option selected   
    setAffixElementWidth();
  });
})
.fail(function(error) {
  console.error(error);
}); 

function draw(data) {
  var y = function(year) {
    var laterYears = data.filter(function (elem) {
      return elem.year > year;
    });

    var numberOfShows = laterYears.map(function (elem) {
      return elem.shows.length;
    }).reduce(function(a, b) {
      return a + b;
    }, 0);

    return laterYears.length + numberOfShows * heightSong;
  };

  var svg = d3.select('.graph')
              .append('svg')
              .attr('width', width)
              .attr('height', height);

  var tip = d3.tip()
              .attr('class', 'd3-tip')
              .offset([-10, 0])
              .html(function(song, venue, date) {
                var text = '<p class="song-name">' + song.name;
                if (song.album) {
                  text += ' <span style="color:#ccc">from</span> ' + song.album;
                }
                text += '</p>';

                text += '<span style="color:#ccc">at</span> ' + venue.name;
                text += ' <span style="color:#ccc">in</span> ' + venue.city;
                if (venue.state) {
                  text += ', ' + venue.state;
                }
                else {
                  text += ', ' + venue.country;
                }

                var dt = moment(date);
                text += ' <span style="color:#ccc">on</span> ' + dt.format('MMMM D, YYYY');
                return text;
              });

  // http://colllor.com/2E8A4B
  // http://bl.ocks.org/Caged/6476579
  svg.call(tip);

  var perYear = svg.selectAll('.per-year')
                   .data(data)
                   .enter()
                   .append('g')
                   .attr('transform', function(showsPerYear) {
                     return 'translate(0,' + (margin.top + y(showsPerYear.year)) + ')';
                   });

  var line = d3.line()
               .x(function(d) { return d.x; })
               .y(function(d) { return d.y; });

  perYear.append('path')
         .attr('d', line([{x: 0, y: 0}, {x: 500, y: 0}]))
         .classed('year-divisor', true);

  var perYearText = perYear.append('text')
                           .attr('x', 0)
                           .attr('y', 25);

  perYearText.append('tspan')
             .attr('x', 0)
             .attr('y', 25)
             .text(function(showsPerYear) { return showsPerYear.year; })
             .classed('year-text', true);
  perYearText.append('tspan')
             .attr('x', 0)
             .attr('y', 60)
             .text(function(showsPerYear) { return showsPerYear.numberOfShows; })
             .classed('stats-text', true);
  perYearText.append('tspan')
             .attr('x', 0)
             .attr('y', 70)
             .text(function(showsPerYear) { return showsPerYear.numberOfShows > 1 ? 'shows' : 'show'; })
             .style('fill', '#ccc');
  perYearText.append('tspan')          
             .attr('x', 0)
             .attr('y', 95)
             .text(function(showsPerYear) { return showsPerYear.numberOfCountries; })
             .classed('stats-text', true);
  perYearText.append('tspan')          
             .attr('x', 0)
             .attr('y', 105)
             .text(function(showsPerYear) { return showsPerYear.numberOfCountries > 1 ? 'countries' : 'country'; })
             .style('fill', '#ccc');

  var shows = perYear.selectAll('g')
                     .data(function(showsPerYear) { return showsPerYear.shows; })
                     .enter()
                     .append('g')
                     .attr('transform', function(show, index) {
                       return 'translate(70,' + (index * heightSong + 1) + ')';
                     });

  shows.selectAll('rect')
       .data(function(show) { return show.songs; })
       .enter()
       .append('rect')
       .style('fill', '#eee')
       .attr('x', function(song, index) { return index * widthSong; })
       .attr('width', widthSong)
       .attr('y', function(song) { return 0; })
       .attr('height', heightSong)
       .attr('class', function(song) {   
         if (song.albumId && song.songId) {
           return 'from-album song-' + song.songId + ' album-' + song.albumId;
         }
       })
       .on('mouseover', function(song) {
         var show = d3.select(this.parentNode).datum();
         tip.show(song, show.venue, show.date);
         if (song.albumId) {
           if (viewModel.selected() === 0 || viewModel.selected() === song.albumId) {
             d3.select(this)
               .attr('opacity', 0.5);
           }
           else {
             d3.select(this)
               .style('fill', '#ccc');
           }
         }
         else {
           d3.select(this)
             .style('fill', '#ccc');
         }
       })
       .on('mouseout', function(song) {
         tip.hide(song);
         if (song.albumId) {
           if (viewModel.selected() === 0 || viewModel.selected() === song.albumId) {
             d3.select(this)
               .attr('opacity', 1);
           }
           else {
             d3.select(this)
               .style('fill', '#eee');
            }
         }
         else {
           d3.select(this)
             .style('fill', '#eee');
         }
       });
}

function subscribe() {
  viewModel.selected.subscribe(function(newValue) {
    if (!newValue) { // 'All' option selected
      viewModel.albums().forEach(function(album) {
        d3.selectAll('.album-' + album.id).style('fill', album.color);
      }, this);
    }
    else {
      var album = viewModel.albums().find(function(album) {
        return album.id === newValue;
      });

      if (!album) return;

      album.songs.forEach(function(song) {
        d3.selectAll('.song-' + song.id).style('fill', song.color);
      }, this);
    }
  });

  viewModel.selected.subscribe(function(oldValue) {
    if (!oldValue) {
      d3.selectAll('.from-album').style('fill', '#eee');
    }
    else {
      var album = viewModel.albums().find(function(album) {
        return album.id === oldValue;
      });

      if (!album) return;

      d3.selectAll('.album-' + album.id).style('fill', '#eee');
    }
  }, null, 'beforeChange');
}

var $affixElement = $("#affix-element");

// https://github.com/twbs/bootstrap/issues/6350#issuecomment-19838255
function setAffixElementWidth() {
    var $parent = $affixElement.parent();
    var width = $parent.width()
        - parseInt($affixElement.css('paddingLeft'))
        - parseInt($affixElement.css('paddingRight'))
        - parseInt($affixElement.css('marginLeft'))
        - parseInt($affixElement.css('marginRight'))
        - parseInt($affixElement.css('borderLeftWidth'))
        - parseInt($affixElement.css('borderRightWidth'));

    $affixElement.width(width);
}

$(window).resize(function() { setAffixElementWidth(); });