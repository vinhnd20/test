<template>
  <div class="d-flex justify-content-center align-items-center m-4">
    <div>
      <div class="h4 mx-2">
        {{ "Sửa thông tin khách hàng " + item.hoTen + " :" }}
      </div>
      <div style="height: 50px"></div>
      <form action="" v-on:submit.prevent="updateItem()">
        <div class="p-5 mx-2 mt-2" style="border: 1px solid #000">
          <div class="my-2">
            <label for="hoTen" class="form-label">Họ tên :</label>
            <input
              type="text"
              id="hoTen"
              class="form-control"
              placeholder="tên sản phẩm muốn thêm"
              v-model="item.hoTen"
              required
            />
          </div>
          <div class="my-2">
            <label for="Email" class="form-label">Email :</label>
            <input
              type="text"
              id="Email"
              class="form-control"
              placeholder="tên sản phẩm muốn thêm"
              v-model="item.email"
              required
            />
          </div>
          <div class="my-2">
            <label for="SoDienThoai" class="form-label">Số điện thoại :</label>
            <input
              type="text"
              id="SoDienThoai"
              class="form-control"
              placeholder="tên sản phẩm muốn thêm"
              v-model="item.soDienThoai"
              required
            />
          </div>
          <div class="my-2">
            <label for="moTa" class="form-label">Địa chỉ</label>
            <textarea
              id="moTa"
              class="form-control"
              style="resize: none"
              rows="3"
              placeholder="Địa chỉ giao hàng"
              v-model="item.diaChi"
              required
            ></textarea>
          </div>
          <div class="d-flex justify-content-center my-4">
            <button type="submit" class="btn btn-primary">Sửa thông tin</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import DVDieuHoaAxios from "~/mixins/dv-dieuhoa-axios";
import Utility from "~/mixins/utility";
export default {
  mixins: [DVDieuHoaAxios, Utility],
  async beforeMount() {
    await this.getItem();
  },
  data() {
    return {
      item: [],
    };
  },
  methods: {
    async getItem() {
      const id = this.$route.query.form;
      var data = await this.Get(`api/QuanLyForm/GetUserByID?id=${id}`, {});
      if (data.code == "Ok") {
        this.item = data.data;
      } else {
        this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    async updateItem() {
      const dataApi = {
        id: this.item.id,
        hoTen: this.item.hoTen,
        email: this.item.email,
        soDienThoai: this.item.soDienThoai,
        diaChi: this.item.diaChi,
        productId: this.item.productId,
      };

      var response = await this.Put("api/QuanLyForm/UpdateUserForm", dataApi);
      if (response.code == "Ok") {
        this.$toast.success("Chúc mừng bạn đã chỉnh sửa form thành công !", {
          autoClose: 1000,
        });
        setTimeout(() => {
          this.$router.push({ path: "/form-user" });
        }, 1000);
      } else {
        this.$toast.error("Sửa form thất bại ! ", {
          autoClose: 1000,
        });
      }
    },
  },
};
</script>
<style>
.form-label {
  font-weight: 600;
}
</style>
