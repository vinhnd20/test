<template>
  <div>
    <button
      class="navbar-toggler px-3 py-2"
      type="button"
      @click="toggleSidebar"
    >
      <font-awesome-icon icon="fa-solid fa-bars" />
    </button>
    <div class="vertical-menu" :class="{ 'sidebar-open': sidebarOpen }">
      <button
        class="navbar-toggler px-3 py-2"
        type="button"
        @click="toggleSidebar"
      >
        <font-awesome-icon icon="fa-solid fa-xmark" />
      </button>
      <div class="nav flex-column">
        <NuxtLink class="nav-link my-2 d-flex" :to="{ path: '/dash-board' }">
          <div class="mx-2">
            <font-awesome-icon icon="fa-solid fa-chart-simple" />
          </div>
          Biểu đồ
        </NuxtLink>
        <NuxtLink class="nav-link my-2 d-flex" :to="{ path: '/form-user' }">
          <div class="mx-2">
            <font-awesome-icon icon="fa-solid fa-user" />
          </div>
          Quản lý form người dùng
        </NuxtLink>
        <NuxtLink class="nav-link my-2 d-flex" :to="{ path: '/item' }"
          ><div class="mx-2">
            <font-awesome-icon icon="fa-solid fa-fan" />
          </div>
          Quản lý sản phẩm
        </NuxtLink>
        <NuxtLink class="nav-link my-2 d-flex" :to="{ path: '/brand' }">
          <div class="mx-2">
            <font-awesome-icon icon="fa-solid fa-phone" />
          </div>
          Quản lý nhãn hàng
        </NuxtLink>
        <div
          class="nav-link my-2 d-flex"
          data-bs-toggle="modal"
          data-bs-target="#logoutModal"
        >
          <div class="mx-2">
            <font-awesome-icon icon="fa-solid fa-arrow-right-from-bracket" />
          </div>
          Đăng xuất
        </div>
      </div>
    </div>
  </div>
  <!-- Modal -->
  <div
    class="modal fade"
    id="logoutModal"
    tabindex="-1"
    aria-labelledby="logoutModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header d-flex justify-content-between">
          <div></div>
          <h5 class="modal-title" id="exampleModalLabel">Đăng xuất</h5>
          <button
            type="button"
            class="btn-close mx-0"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">Bạn có muốn đăng xuất khỏi thiết bị ?</div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-primary"
            value="close"
            data-bs-dismiss="modal"
            @click="logout"
          >
            Có
          </button>
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Không
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Utility from "~/mixins/utility";
export default {
  mixins: [Utility],
  data() {
    return {
      menuItemOpen: null,
      sidebarOpen: false,
    };
  },
  methods: {
    toggleSidebar() {
      this.sidebarOpen = !this.sidebarOpen;
    },
    toggleMenuItem(index) {
      this.menuItemOpen = this.menuItemOpen === index ? null : index;
    },
    async logout() {
      this.deleteAllLocalStorages();
      this.deleteAllCookies();
      this.$router.push({ path: "/login" });
    },
  },
};
</script>

<style scoped>
.navbar-toggler:hover {
  background-color: #c3ccd5;
  border-radius: 8px;
}
.vertical-menu {
  width: 250px;
  position: fixed;
  left: -250px;
  top: 0;
  bottom: 0;
  background-color: #f8f9fa;
  overflow-y: auto;
  transition: left 0.3s ease-out;
  z-index: 1;
}

.sidebar-open {
  left: 0;
}

.nav-link {
  cursor: pointer;
  border: #d0d7de solid 1px;
  border-left: 0;
  border-right: 0;
}

.nav-link:hover {
  background-color: #f0f1f5;
}

.navbar-toggler {
  font-size: 30px;
  background-color: #f8f9fa;
  font-weight: 100;
  margin: 20px;
}

.submenu {
  padding-left: 20px;
  overflow: hidden;
  transition: max-height 0.3s ease-out;
}

.submenu a {
  padding-left: 20px;
}

.submenu a:hover {
  background-color: #f0f0f0;
}
</style>
