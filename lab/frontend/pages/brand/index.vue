<template>
  <div class="mx-4 mt-4">
    <div cla class="mt-3">
      <NuxtLink :to="{ path: '/create-brand' }"
        ><button type="button" class="btn btn-primary">
          Thêm mới
        </button></NuxtLink
      >
    </div>
    <div v-if="checkKey" class="h3 text-center m-4" style="color: red">
      Không có hãng nào được tìm kiếm !
    </div>
    <table v-else class="table table-bordered mt-3">
      <thead>
        <tr style="text-align: center">
          <th scope="col">Mã hãng</th>
          <th scope="col">Tên hãng</th>
          <th scope="col">Mô tả</th>

          <th scope="col">Ảnh minh hoạ</th>
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
          <td>{{ i.tenHang }}</td>
          <td>{{ i.moTa }}</td>
          <td>
            <img :src="[formatImage(i.image)]" style="height: 60px" alt="" />
          </td>
          <td>
            <div class="d-flex align-items-center justify-content-center">
              <NuxtLink
                class="px-2 py-1 m-2 edit-icon"
                :to="{
                  path: '/update-brand',
                  query: { brand: i.id },
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
                  <h5 class="modal-title" id="exampleModalLabel">Xoá hãng</h5>
                  <button
                    type="button"
                    class="btn-close mx-0"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <div class="modal-body">
                  {{ "Bạn có muốn xoá " + i.tenHang + " ra khỏi danh sách ?" }}
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
    };
  },
  methods: {
    async getItem() {
      var data = await this.Get("api/Hang/GetAllBrand", {});
      if (data.code == "Ok") {
        this.items = data.data;
      } else {
        this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    async deleteItem(id) {
      var res = await this.Delete(`api/Hang/HideBrand?HangId=${id}`, {});
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
    // async seacrhItem(keySearch) {
    //   var data = await this.Get(
    //     `api/SanPham/GetProductByName?name=${keySearch}`,
    //     {}
    //   );
    //   if (data.code == "Ok") {
    //     if (data.data == null) {
    //       this.checkKey = true;
    //     } else {
    //       this.checkKey = false;
    //       this.items = data.data;
    //     }
    //   } else {
    //     this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
    //       autoClose: 1000,
    //     });
    //   }
    // },
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
