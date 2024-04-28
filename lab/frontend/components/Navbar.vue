<template>
  <div class="w-100 d-flex">
    <div
      class="d-flex align-items-center px-4"
      style="background-color: #f8f9fa"
    >
      <Menu></Menu>
    </div>

    <nav class="navbar navbar-expand-lg navbar-light bg-light w-100">
      <div class="container-fluid d-flex align-items-center">
        <!-- Logo -->
        <a class="navbar-brand mt-2" href="/dash-board">
          <img src="~/assets/images/logos/logocty.png" alt="Logo" height="50" />
        </a>

        <!-- Toggle button for mobile -->
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>

        <!-- Navbar content -->
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <!-- <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <a class="nav-link" href="#">Trang chủ</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">About</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Contact</a>
            </li>
          </ul> -->

          <!-- Search form -->
          <form class="d-flex" v-on:submit.prevent="search()">
            <input
              class="form-control me-2"
              type="search"
              placeholder="Tìm kiếm"
              aria-label="Search"
              v-model="keySearch"
              required
            />
            <button class="btn btn-outline-success" type="submit">
              <font-awesome-icon icon="fa-solid fa-magnifying-glass" />
            </button>
          </form>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  data() {
    return {
      keySearch: "",
    };
  },
  methods: {
    checkMenu() {},
    search() {
      this.$router.push({
        path: "/item",
        query: { keySearch: this.keySearch },
      });
    },
  },
};
</script>

<style scoped>
/* Add your custom styles here */
</style>
