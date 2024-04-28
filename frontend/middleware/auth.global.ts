export default defineNuxtRouteMiddleware((to, from) => {
  const token = useCookie("dieuhoa-token");
  const serectKey = useCookie("dieuhoa-key-secret");
  const emailInForgotPassword = useCookie("dieuhoa-email");
  if (token.value) {
    if (to.path == "/login") {
      return navigateTo("/");
    }
  } else {
    if (
      to.path != "/login" &&
      to.path != "/forgot-password" &&
      to.path != "/get-new-password"
    ) {
      return navigateTo("/login");
    }
    if (to.path == "/get-new-password") {
      if (!serectKey.value || !emailInForgotPassword.value) {
        return navigateTo("/login");
      }
    }
  }
  // if (to.path != "/login" && to.path != "/signup") {
  //   return navigateTo("/login");
  // }
});
