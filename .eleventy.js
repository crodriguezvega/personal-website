module.exports = function(eleventyConfig) {
  // Passthrough copy: static assets
  eleventyConfig.addPassthroughCopy({ "src/public/css": "css" });
  eleventyConfig.addPassthroughCopy({ "src/public/js": "js" });
  eleventyConfig.addPassthroughCopy({ "src/public/img": "img" });
  eleventyConfig.addPassthroughCopy({ "src/public/json": "json" });
  eleventyConfig.addPassthroughCopy({ "src/public/favicon": "favicon" });

  // Vendor libraries: map to root-level paths to preserve URL structure
  eleventyConfig.addPassthroughCopy({ "src/vendor/jquery": "jquery" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/bootstrap": "bootstrap" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/d3": "d3" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/d3-tip": "d3-tip" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/d3-format": "d3-format" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/spin": "spin" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/knockout": "knockout" });
  eleventyConfig.addPassthroughCopy({ "src/vendor/moment": "moment" });

  return {
    dir: {
      input: "src",
      includes: "_includes",
      data: "_data",
      output: "_site"
    },
    templateFormats: ["njk"],
    htmlTemplateEngine: "njk"
  };
};
