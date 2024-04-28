<template>
  <div class="d-flex justify-content-center align-items-center m-4">
    <div>
      <div class="h4 mx-2">{{ "Sửa thông tin " + item.ten + " :" }}</div>
      <div style="height: 50px"></div>
      <form action="" v-on:submit.prevent="updateItem()">
        <div class="d-lg-flex justify-content-between d-block">
          <div class="p-5 mx-2 mt-2" style="border: 1px solid #000">
            <div class="my-2">
              <label for="tenSP" class="form-label">Tên sản phẩm :</label>
              <input
                type="text"
                id="tenSP"
                class="form-control"
                placeholder="tên sản phẩm muốn thêm"
                v-model="item.ten"
                required
              />
            </div>
            <div class="my-2">
              <label for="giaTien" class="form-label">Giá tiền :</label>
              <input
                type="number"
                id="giaTien"
                class="form-control"
                placeholder="Giá của sản phẩm"
                v-model="item.giaTien"
                required
              />
            </div>
            <div class="d-flex my-2">
              <div class="me-2">
                <label for="soLuong" class="form-label">Số lượng :</label>
                <input
                  type="number"
                  id="soLuong"
                  class="form-control"
                  placeholder="Số lượng sản phẩm"
                  v-model="item.soLuong"
                  required
                />
              </div>
              <div>
                <label for="Model" class="form-label">Model :</label>
                <input
                  type="text"
                  id="soLuong"
                  class="form-control"
                  placeholder="Model sản phẩm"
                  v-model="item.model"
                  required
                />
              </div>
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
          </div>
          <div class="p-5 mt-2" style="border: 1px solid #000">
            <div class="my-2">
              <label for="hang" class="form-label">Hãng :</label>
              <select
                class="form-select"
                id="hang"
                aria-label="Default select example"
                v-model="item.hangID"
              >
                <option disabled selected>Hãng</option>
                <option v-if="checkBrand" value="" disabled>
                  Không tìm thấy hãng
                </option>
                <option v-else v-for="b in brands" :value="b.id">
                  {{ b.tenHang }}
                </option>
              </select>
            </div>

            <div class="my-2">
              <label for="kieuDang" class="form-label">Kiểu dáng :</label>
              <select
                class="form-select"
                id="kieuDang"
                aria-label="Default select example"
                v-model="item.kieuDang"
              >
                <option selected disabled>Kiểu dáng</option>
                <option value="Âm trần">Âm trần</option>
                <option value="Treo tường">Treo tường</option>
              </select>
            </div>
            <div class="d-flex justify-content-between my-2">
              <div class="me-2">
                <label for="soChieu" class="form-label">Số chiều :</label>
                <select
                  class="form-select"
                  id="soChieu"
                  aria-label="Default select example"
                  v-model="item.soChieu"
                >
                  <option selected disabled>Số chiều</option>
                  <option value="1">1 chiều</option>
                  <option value="2">2 chiều</option>
                </select>
              </div>
              <div class="me-2">
                <label for="congSuat" class="form-label">Công suất :</label>
                <select
                  class="form-select"
                  id="congSuat"
                  aria-label="Default select example"
                  v-model="item.congSuat"
                >
                  <option selected disabled>Công suất</option>
                  <option value="9000">9000</option>
                  <option value="12000">12000</option>
                  <option value="18000">18000</option>
                  <option value="24000BTU">24000BTU</option>
                </select>
              </div>
              <div>
                <label for="trangThai" class="form-label">Trạng thái :</label>
                <select
                  class="form-select"
                  id="trangThai"
                  aria-label="Default select example"
                  v-model="item.trangThai"
                >
                  <option selected disabled>Trạng thái</option>
                  <option value="0">Cũ</option>
                  <option value="1">Mới</option>
                </select>
              </div>
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
              <button type="submit" class="btn btn-primary">
                Sửa thông tin
              </button>
            </div>
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
    await this.getBrand();
  },
  data() {
    return {
      item: [],
      image: [],
      brands: [],
      checkBrand: false,
    };
  },
  methods: {
    async getItem() {
      const id = this.$route.query.item;
      var data = await this.Get(`api/SanPham/GetProductById?Id=${id}`, {});
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
      formData.append("id", this.$route.query.item);
      formData.append("ten", this.item.ten);
      console.log(this.image[0][0].name);
      if (this.image[0][0].name != this.item.image) {
        formData.append(
          "Image",
          this.image[0][0].file,
          this.image[0][0].file.name
        );
      }
      formData.append("giaTien", this.item.giaTien);
      formData.append("moTa", this.item.moTa);
      formData.append("soLuong", this.item.soLuong);
      formData.append("congSuat", this.item.congSuat);
      formData.append("kieuDang", this.item.kieuDang);
      formData.append("model", this.item.model);
      formData.append("soChieu", this.item.soChieu);
      formData.append("HangID", this.item.hangID);
      formData.append("TrangThai", this.item.trangThai);
      var response = await this.PutFormData(
        "api/SanPham/UpdateProduct",
        formData
      );
      if (response.code == "Ok") {
        this.$toast.success(
          "Chúc mừng bạn đã chỉnh sửa sản phẩm thành công !",
          {
            autoClose: 1000,
          }
        );
        setTimeout(() => {
          this.$router.push({ path: "/item" });
        }, 1000);
      } else {
        this.$toast.error("Sửa sản phẩm thất bại ! ", {
          autoClose: 1000,
        });
      }
    },
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
  },
};
</script>
<style>
.form-label {
  font-weight: 600;
}
</style>
