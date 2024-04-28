<template>
  <div class="mx-4 mt-4">
    <div
      style="
        background-color: #f1f1f1;
        border: 1px solid #d9d9d9;
        border-left: 0;
        border-right: 0;
      "
    >
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
                    <label for="hang" class="form-label">Hãng :</label>
                    <select
                      class="form-select"
                      id="hang"
                      aria-label="Default select example"
                      v-model="filter.hangId"
                    >
                      <option value="0">Hãng</option>
                      <option v-if="checkBrand" value="" disabled>
                        Không tìm thấy hãng
                      </option>
                      <option v-else v-for="b in brands" :value="b.id">
                        {{ b.tenHang }}
                      </option>
                    </select>
                  </div>
                  <div class="col-6 col-md-auto mx-2 mb-2">
                    <label for="CongSuat" class="form-label">Công suất :</label>
                    <select
                      class="form-select"
                      aria-label="Default select example"
                      v-model="filter.congSuat"
                      id="CongSuat"
                    >
                      <option value="">Công suất</option>
                      <option value="9000">9000</option>
                      <option value="12000">12000</option>
                      <option value="18000">18000</option>
                      <option value="24000BTU">24000BTU</option>
                    </select>
                  </div>
                  <div class="col-6 col-md-auto mx-2 mb-2">
                    <label for="soChieu" class="form-label">Số chiều :</label>
                    <select
                      id="soChieu"
                      class="form-select"
                      aria-label="Default select example"
                      v-model="filter.soChieu"
                    >
                      <option value="">Số chiều</option>
                      <option value="1">1 chiều</option>
                      <option value="2">2 chiều</option>
                    </select>
                  </div>
                  <div class="col-6 col-md-auto mx-2 mb-2">
                    <label for="kieuDang" class="form-label">Kiểu dáng :</label>
                    <select
                      id="kieuDang"
                      class="form-select"
                      aria-label="Default select example"
                      v-model="filter.kieuDang"
                    >
                      <option value="">Kiểu dáng</option>
                      <option value="Âm trần">Âm trần</option>
                      <option value="Treo tường">Treo tường</option>
                    </select>
                  </div>
                  <div class="col-6 col-md-auto mx-2 mb-2">
                    <label for="giaTien" class="form-label">Giá tiền :</label>
                    <select
                      id="giaTien"
                      class="form-select"
                      aria-label="Default select example"
                      v-model="filter.giaDauCuoi"
                    >
                      <option :value="{ giaDau: 0, giaCuoi: 0 }">
                        Giá tiền
                      </option>
                      <option :value="{ giaDau: 1, giaCuoi: 10000000 }">
                        Dưới 10 triệu
                      </option>
                      <option :value="{ giaDau: 10000001, giaCuoi: 100000000 }">
                        Trên 10 triệu
                      </option>
                    </select>
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
    </div>
    <div cla class="mt-3">
      <NuxtLink :to="{ path: '/create-item' }"
        ><button type="button" class="btn btn-primary">
          Thêm mới
        </button></NuxtLink
      >
    </div>
    <div v-if="checkKey" class="h3 text-center m-4" style="color: red">
      Không có sản phẩm được tìm kiếm !
    </div>
    <table v-else class="table table-bordered mt-3">
      <thead>
        <tr style="text-align: center">
          <th scope="col">Mã sản phẩm</th>
          <th scope="col">Tên sản phẩm</th>
          <th scope="col">Giá tiền</th>
          <th scope="col">Mô tả</th>
          <th scope="col">Công suất</th>
          <th scope="col">Kiểu dáng</th>
          <th scope="col">Model</th>
          <th scope="col">Số chiều</th>
          <th scope="col">Ảnh minh hoạ</th>
          <th scope="col">Thao tác</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(i, index) in paginatedItems.reverse()"
          :key="index"
          style="text-align: center; vertical-align: middle"
        >
          <th scope="row">{{ i.id }}</th>
          <td>{{ i.ten }}</td>
          <td>{{ i.giaTien }}</td>
          <td>{{ i.moTa }}</td>
          <td>{{ i.congSuat }}</td>
          <td>{{ i.kieuDang }}</td>
          <td>{{ i.model }}</td>
          <td>{{ i.soChieu }}</td>
          <td>
            <img :src="[formatImage(i.image)]" style="height: 60px" alt="" />
          </td>
          <td>
            <div class="d-flex align-items-center justify-content-center">
              <NuxtLink
                class="px-2 py-1 m-2 edit-icon"
                :to="{
                  path: '/update-item',
                  query: { item: i.id },
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
                    Xoá sản phẩm
                  </h5>
                  <button
                    type="button"
                    class="btn-close mx-0"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <div class="modal-body">
                  {{ "Bạn có muốn xoá " + i.ten + " ra khỏi danh sách ?" }}
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
    await this.getBrand();
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
      brands: [],
      currentPage: 1,
      itemsPerPage: 10,
      checkKey: false,
      checkBrand: false,
      filter: {
        hangId: 0,
        congSuat: "",
        kieuDang: "",
        soChieu: "",
        giaDauCuoi: {
          giaDau: 0,
          giaCuoi: 0,
        },
      },
    };
  },
  methods: {
    async getBrand() {
      var data = await this.Get("api/Hang/GetAllBrand", {});
      if (data.code == "Ok") {
        if (data.data == null) {
          this.checkBrand = true;
        } else {
          this.checkBrand = false;
          this.brands = data.data;
        }
      } else {
        this.$toast.error("Lỗi lấy dữ liệu hãng !", {
          autoClose: 1000,
        });
      }
    },
    async findByFilter() {
      const apiData = {
        hangId: this.filter.hangId,
        congSuat: this.filter.congSuat,
        kieuDang: this.filter.kieuDang,
        soChieu: this.filter.soChieu,
        giaDau: this.filter.giaDauCuoi.giaDau,
        giaCuoi: this.filter.giaDauCuoi.giaCuoi,
      };
      var data = await this.Post("api/SanPham/FindByFilter", apiData);
      if (data.code == "Ok") {
        this.$toast.success("Tìm kiếm thành công !", {
          autoClose: 1000,
        });
        this.items = data.data;

        this.$router.replace({
          path: "/item",
        });
      } else {
        this.$toast.error("Lỗi tìm kiếm bằng bộ lọc !", {
          autoClose: 1000,
        });
      }
    },
    async getItem() {
      var data = await this.Get("api/SanPham/GetAllProduct", {});
      if (data.code == "Ok") {
        this.items = data.data;
      } else {
        this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    async seacrhItem(keySearch) {
      var data = await this.Get(
        `api/SanPham/GetProductByName?name=${keySearch}`,
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
    async deleteItem(id) {
      var res = await this.Delete(
        `api/SanPham/DeleteProduct?ProductID=${id}`,
        {}
      );
      if (res.code === "Ok") {
        this.$toast.success("Xoá sản phẩm thành công !", {
          autoClose: 1000,
        });
        setTimeout(() => {
          window.location.reload();
        }, 1000);
      } else {
        this.$toast.error("Xoá sản phẩm thất bại !", {
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
