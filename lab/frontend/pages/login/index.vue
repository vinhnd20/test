<template>
  <div
    class="d-flex justify-content-center align-items-center vh-100"
    style="font-family: sans-serif; background-color: #f1f3f4"
  >
    <div>
      <div
        style="
          padding: 60px 80px;
          border: 1px #fff solid;
          border-radius: 8px;
          background-color: #fff;
        "
      >
        <div class="mb-4 d-flex align-items-center justify-content-center">
          <img
            src="~/assets/images/logos/logocty.png"
            alt=""
            class="login-logocty"
          />
        </div>
        <div
          class="h2 my-4 d-flex justify-content-center"
          style="font-weight: 600"
        >
          Dịch vụ điều hoà
        </div>

        <form v-on:submit.prevent="login()">
          <div class="mb-3">
            <label for="exampleInputEmail1" class="form-label">Tài khoản</label>
            <input
              type="email"
              class="form-control"
              id="exampleInputEmail1"
              aria-describedby="emailHelp"
              v-model="email"
              required
            />
            <div id="emailHelp" class="form-text">
              Vui lòng không chia sẻ email cho người khác.
            </div>
          </div>
          <div class="mb-3">
            <label for="exampleInputPassword1" class="form-label"
              >Mật khẩu</label
            >
            <input
              type="password"
              class="form-control"
              id="exampleInputPassword1"
              v-model="password"
              required
            />
          </div>
          <div class="mb-2 d-flex justify-content-between">
            <div class="form-check">
              <input
                type="checkbox"
                class="form-check-input"
                id="exampleCheck1"
              />
              <label class="form-check-label" for="exampleCheck1"
                >Ghi nhớ</label
              >
            </div>
            <div class="d-none d-sm-block">
              <a href="">Quên mật khẩu ?</a>
            </div>
          </div>
          <div class="d-block d-sm-none mb-3">
            <a href="" style="text-decoration: none">Quên mật khẩu ?</a>
          </div>
          <div class="justify-content-center d-flex">
            <button
              type="submit"
              class="btn"
              style="background-color: #726ab5; color: #fff"
            >
              Đăng nhập
            </button>
          </div>
        </form>
        <div style="height: 50px"></div>
      </div>
    </div>
    <!-- loading -->
    <div v-if="loading" class="loading-customer p-0">
      <div>
        <div
          class="w-100 vh-100 d-flex justify-content-center align-items-center"
          style="background-color: rgba(0, 0, 0, 0.3)"
        >
          <div
            class="spinner-border text-primary"
            role="status"
            style="width: 50px; height: 50px; font-size: 20px"
          >
            <span class="sr-only">Loading...</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
definePageMeta({
  layout: "none-layout",
});
import DVDieuHoaAxios from "~/mixins/dv-dieuhoa-axios";
export default {
  mixins: [DVDieuHoaAxios],
  components: {},
  async beforeMount() {},

  data() {
    return {
      email: "",
      password: "",
      loading: false,
    };
  },
  methods: {
    async login() {
      this.loading = true;
      const loginRequest = {
        userName: this.email,
        password: this.password,
      };

      var data = await this.Post(
        "api/UserAuthenticate/UserLogin",
        loginRequest
      );
      if (data.code == "Ok") {
        //save data to cookie
        const d = new Date();
        d.setTime(d.getTime() + 1 * 24 * 60 * 60 * 1000);
        let expires = "expires=" + d.toUTCString();
        document.cookie =
          "dieuhoa-token=" + data.data.token + ";" + expires + ";path=/";
        this.loading = false;
        this.$toast.success("Login successfull !", {
          autoClose: 1000,
        });
        setTimeout(() => {
          this.$router.push({ path: "/dash-board" });
          reloadNuxtApp();
        }, 1000);
      } else {
        // alert(data.des);
        this.loading = false;

        this.$toast.error(`${data.des}`, {
          autoClose: 1000,
        });
      }
    },
  },
};
</script>

<style scoped>
.login-logocty {
  /* width: 20px; */
  height: 200px;
}
.loading-customer {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
}
</style>
