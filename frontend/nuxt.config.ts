import { defineNuxtConfig } from "nuxt";

export default defineNuxtConfig({
  ssr: false,
  modules: ["@inkline/nuxt"],
  inkline: {
    colorMode: "light",
  },
  components: {
    global: true,
    dirs: ["~/components"],
  },
  build: {
    transpile: ["@vuepic/vue-datepicker"],
  },
});
