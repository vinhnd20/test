<template>
  <div class="mx-4 mt-4">
    <div class="m-2 d-flex align-items-center">
      <a
        class="btn btn-collapsee p-2 me-3"
        data-bs-toggle="collapse"
        href="#collapseExample"
        role="button"
        aria-expanded="false"
        aria-controls="collapseExample"
        style="color: #737373; font-weight: 600"
      >
        <div class="d-flex">
          <div class="pe-3 d-flex">
            <div class="me-2">
              <font-awesome-icon icon="fa-solid fa-filter" />
            </div>
            Bộ lọc
          </div>
          <div><font-awesome-icon icon="fa-solid fa-caret-down" /></div>
        </div>
      </a>
      <div>
        <button class="btn btn-success" @click="getItem()">
          Hiển thị tất cả
        </button>
      </div>
    </div>
    <div class="collapse m-4" id="collapseExample">
      <div class="card card-body">
        <div class="d-flex justify-content-center align-items-center">
          <div>
            <form action="" v-on:submit.prevent="findByFilter()">
              <div class="d-flex flex-wrap justify-content-center">
                <div class="col-6 col-md-auto mx-2 mb-2">
                  <label for="tuNgay" class="form-label">Từ ngày :</label>
                  <input
                    v-model="filter.tuNgay"
                    type="datetime-local"
                    class="form-control"
                    id="tuNgay"
                  />
                </div>
                <div class="col-6 col-md-auto mx-2 mb-2">
                  <label for="denNgay" class="form-label">Đến ngày :</label>
                  <input
                    v-model="filter.denNgay"
                    type="datetime-local"
                    class="form-control"
                    id="denNgay"
                  />
                </div>
                <div class="col-6 col-md-auto mx-2 mb-2">
                  <label for="hoTen" class="form-label">Tên :</label>
                  <input
                    v-model="filter.hoTen"
                    type="text"
                    class="form-control"
                    id="hoTen"
                  />
                </div>
                <div class="col-6 col-md-auto mx-2 mb-2">
                  <label for="soDienThoai" class="form-label"
                    >Số điện thoại :</label
                  >
                  <input
                    v-model="filter.soDienThoai"
                    type="text"
                    class="form-control"
                    id="soDienThoai"
                  />
                </div>
              </div>
              <div
                class="mx-2 mt-2 d-flex justify-content-center align-items-center"
              >
                <button type="submit" class="btn btn-secondary">
                  Tìm kiếm
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    <div class="mx-2 h5" style="color: red">
      Thông tin khách hàng sẽ hạn chế sửa hoặc xoá !
    </div>
    <div class="mx-2 h5" style="color: red">
      Tải lại trang để cập nhật thông tin mới nhất.
    </div>
    <table class="table table-bordered">
      <thead>
        <tr style="text-align: center">
          <th scope="col">#</th>
          <th scope="col">Tên khách hàng</th>
          <th scope="col">Email</th>
          <th scope="col">Số điện thoại</th>
          <th scope="col">Địa chỉ giao hàng</th>
          <th scope="col">Sản phẩm</th>
          <th scope="col">Ngày gửi</th>
          <th scope="col">Thao tác</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(i, index) in paginatedItems"
          :key="index"
          style="text-align: center; vertical-align: middle"
        >
          <th scope="row">{{ i.id }}</th>
          <td>{{ i.hoTen }}</td>
          <td>{{ i.email }}</td>
          <td>{{ i.soDienThoai }}</td>
          <td>{{ i.diaChi }}</td>
          <td>{{ i.productName }}</td>
          <td>{{ formatDate(i.createdDate) }}</td>
          <td>
            <div class="d-flex align-items-center justify-content-center">
              <NuxtLink
                class="px-2 py-1 m-2 edit-icon"
                :to="{
                  path: '/update-form-user',
                  query: { form: i.id },
                }"
              >
                <font-awesome-icon
                  icon="fa-solid fa-pen-to-square"
                  style="color: #0d6efd"
                />
              </NuxtLink>
              <div
                class="px-2 py-1 edit-icon"
                data-bs-toggle="modal"
                :data-bs-target="'#deleteModal-' + i.id"
              >
                <font-awesome-icon
                  icon="fa-solid fa-trash"
                  style="color: red"
                />
              </div>
            </div>
          </td>
          <!-- Modal -->
          <div
            class="modal fade"
            :id="'deleteModal-' + i.id"
            tabindex="-1"
            aria-labelledby="deleteModalLabel"
            aria-hidden="true"
          >
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header d-flex justify-content-between">
                  <div></div>
                  <h5 class="modal-title" id="exampleModalLabel">
                    Xoá thông tin
                  </h5>
                  <button
                    type="button"
                    class="btn-close mx-0"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <div class="modal-body">
                  {{
                    "Bạn có muốn xoá thông tin khách hàng " +
                    i.hoTen +
                    " ra khỏi danh sách ?"
                  }}
                </div>
                <div class="modal-footer">
                  <button
                    type="button"
                    class="btn btn-primary"
                    value="close"
                    data-bs-dismiss="modal"
                    @click="deleteItem(i.id)"
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
        </tr>
      </tbody>
    </table>
    <div class="d-flex justify-content-center">
      <button
        class="btn btn-primary mx-1"
        @click="prevPage"
        :disabled="currentPage === 1"
      >
        Trang trước
      </button>
      <button
        class="btn btn-primary mx-1"
        @click="nextPage"
        :disabled="currentPage === totalPages"
      >
        Trang tiếp
      </button>
    </div>
  </div>
</template>
<script>
import DVDieuHoaAxios from "~/mixins/dv-dieuhoa-axios";
import Utility from "~/mixins/utility";
export default {
  mixins: [DVDieuHoaAxios, Utility],
  async beforeMount() {
    const key = this.$route.query.keySearch;
    if (key) {
      await this.seacrhItem(key);
    } else {
      await this.getItem();
    }
  },
  data() {
    return {
      items: [],
      currentPage: 1,
      itemsPerPage: 10,
      checkKey: false,
      filter: {
        hoTen: "",
        soDienThoai: "",
        tuNgay: null,
        denNgay: null,
      },
    };
  },
  methods: {
    async getItem() {
      var data = await this.Get("api/QuanLyForm/GetAllUser", {});
      if (data.code == "Ok") {
        this.items = data.data;
      } else {
        this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    async deleteItem(id) {
      var res = await this.Delete(`api/QuanLyForm/DeleteForm?id=${id}`, {});
      if (res.code === "Ok") {
        this.$toast.success("Xoá nhãn hàng thành công !", {
          autoClose: 1000,
        });
        setTimeout(() => {
          window.location.reload();
        }, 1000);
      } else {
        this.$toast.error("Xoá nhãn hàng thất bại !", {
          autoClose: 1000,
        });
      }
    },
    async seacrhItem(keySearch) {
      var data = await this.Get(
        `api/QuanLyForm/GetUserByName?name=${keySearch}`,
        {}
      );
      if (data.code == "Ok") {
        if (data.data == null) {
          this.checkKey = true;
        } else {
          this.checkKey = false;
          this.items = data.data;
        }
      } else {
        this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    async findByFilter() {
      var data = await this.Post(
        "api/QuanLyForm/FindFormByFilter",
        this.filter
      );
      if (data.code == "Ok") {
        this.$toast.success("Tìm kiếm thành công !", {
          autoClose: 1000,
        });
        this.items = data.data;

        this.$router.replace({
          path: "/form-user",
        });
      } else {
        this.$toast.error("Lỗi tìm kiếm bằng bộ lọc !", {
          autoClose: 1000,
        });
      }
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    changePage(pageNumber) {
      this.currentPage = pageNumber;
    },
  },
  computed: {
    totalPages() {
      return Math.ceil(this.items.length / this.itemsPerPage);
    },
    paginatedItems() {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      return this.items.slice(startIndex, endIndex);
    },
  },
  async beforeRouteUpdate(to, from, next) {
    if (to.query.keySearch) {
      await this.seacrhItem(to.query.keySearch);
    } else {
      await this.getItem();
    }
    next();
  },
};
</script>
<style scoped>
.edit-icon:hover {
  background-color: #f0f1f5;
  cursor: pointer;
  border-radius: 8px;
}
.btn-collapsee {
  background-color: #fff;
  border: 1px solid #000;
}
</style>
