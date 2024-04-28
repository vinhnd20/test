import VueFileAgentNext from "@boindil/vue-file-agent-next";

import "@boindil/vue-file-agent-next/dist/vue-file-agent-next.css";

export default defineNuxtPlugin((nuxtApp) => {
  nuxtApp.vueApp.use(VueFileAgentNext);
});
