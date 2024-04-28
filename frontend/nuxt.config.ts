// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  css: [
    "bootstrap/dist/css/bootstrap.min.css",
    "@fortawesome/fontawesome-svg-core/styles.css",
  ],
  runtimeConfig: {
    public: {
      BaseUrl: "http://localhost:51926/",
    },
  },
  app: {
    head: {
      script: [
        {
          src: "https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js",
        },
      ],
      title: "Dịch vụ điều hoà",
    },
  },
});
