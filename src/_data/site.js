module.exports = function() {
  return {
    year: new Date().getFullYear(),
    assetsUrl: process.env.ELEVENTY_ENV === 'production'
      ? 'https://d2gkkp7311831a.cloudfront.net' : ''
  };
};
