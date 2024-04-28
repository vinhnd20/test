<template>
  <div class="d-flex justify-content-center align-items-center m-4">
    <div>
      <div class="h4 mx-2">{{ "Sửa thông tin " + item.tenHang + " :" }}</div>
      <div style="height: 50px"></div>
      <form action="" v-on:submit.prevent="updateItem()">
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
              v-model:raw-model-value="image[0]"
            ></VueFileAgent>
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
      image: [],
    };
  },
  methods: {
    async getItem() {
      const id = this.$route.query.brand;
      var data = await this.Get(`api/Hang/GetBrandById?Id=${id}`, {});
      if (data.code == "Ok") {
        this.item = data.data;
        this.image[0] = this.convertUrlToFile(this.item.image);
      } else {
        this.$toast.error("Lỗi lấy dữ liệu sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    async updateItem() {
      const formData = new FormData();
      formData.append("id", this.$route.query.brand);
      formData.append("ten", this.item.tenHang);
      console.log(this.image[0][0].name);
      if (this.image[0][0].name != this.item.image) {
        formData.append(
          "image",
          this.image[0][0].file,
          this.image[0][0].file.name
        );
      }
      formData.append("moTa", this.item.moTa);

      var response = await this.PutFormData("api/Hang/UpdateBrand", formData);
      if (response.code == "Ok") {
        this.$toast.success("Chúc mừng bạn đã chỉnh sửa hãng thành công !", {
          autoClose: 1000,
        });
        setTimeout(() => {
          this.$router.push({ path: "/brand" });
        }, 1000);
      } else {
        this.$toast.error("Sửa hãng thất bại ! ", {
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
