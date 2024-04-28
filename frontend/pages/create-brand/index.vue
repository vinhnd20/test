<template>
  <div class="d-flex justify-content-center align-items-center m-4">
    <div>
      <div class="h4 mx-2">Thêm thông tin hãng ở phía dưới :</div>
      <form action="" v-on:submit.prevent="createItem()">
        <div class="p-5 mx-2 mt-2" style="border: 1px solid #000">
          <div class="my-2">
            <label for="tenSP" class="form-label">Tên hãng :</label>
            <input
              type="text"
              id="tenSP"
              class="form-control"
              placeholder="tên sản phẩm muốn thêm"
              v-model="item.tenHang"
              required
            />
          </div>

          <div class="my-2">
            <label for="moTa" class="form-label">Mô tả</label>
            <textarea
              id="moTa"
              class="form-control"
              style="resize: none"
              rows="3"
              placeholder="Mô tả sản phẩm"
              v-model="item.moTa"
              required
            ></textarea>
          </div>
          <div class="my-2">
            <label for="image" class="form-label">Ảnh :</label>
            <VueFileAgent
              id="image"
              :uploadUrl="uploadUrl"
              v-model:raw-model-value="image"
            ></VueFileAgent>
          </div>
          <div class="d-flex justify-content-center my-4">
            <button type="submit" class="btn btn-primary">Tạo mới</button>
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
  async beforeMount() {},
  data() {
    return {
      item: {
        tenHang: "",
        moTa: "",
      },
      image: [],
    };
  },
  methods: {
    async createItem() {
      const formData = new FormData();
      formData.append("tenHang", this.item.tenHang);
      formData.append("image", this.image[0].file, this.image[0].file.name);
      formData.append("moTa", this.item.moTa);

      const response = await this.PostFormData("api/Hang/AddBrand", formData);
      if (response.code == "Ok") {
        this.$toast.success("Thêm Hãng thành công !", {
          autoClose: 1000,
        });
        setTimeout(() => {
          this.$router.push({ path: "/brand" });
        }, 1000);
      } else {
        this.$toast.error("Lỗi thêm hãng ", {
          autoClose: 1000,
        });
      }
    },
  },
};
</script>
<style scoped>
.form-label {
  font-weight: 600;
}
</style>
