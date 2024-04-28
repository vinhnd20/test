<template>
  <div class="d-flex justify-content-center m-4">
    <div>
      <div class="p-4 bg-light" style="border-radius: 8px">
        <div class="mb-3 h5">
          Biểu đồ thống kê lượng người dùng truy cập trong một tuần qua :
        </div>
        <canvas style="height: 500px" ref="lineChartCanvasInUser"></canvas>
      </div>
      <div class="p-4 bg-light mt-4" style="border-radius: 8px">
        <div class="mb-3 h5">
          Biểu đồ thống kê thêm sản phẩm trong một tuần qua :
        </div>
        <canvas style="height: 500px" ref="lineChartCanvasInItem"></canvas>
      </div>
    </div>
  </div>
</template>

<script>
import Chart from "chart.js/auto";
import DVDieuHoaAxios from "~/mixins/dv-dieuhoa-axios";
import Utility from "~/mixins/utility";
export default {
  mixins: [DVDieuHoaAxios, Utility],
  async beforeMount() {
    await this.getProductStatisticskInWeek();
  },
  name: "LineChart",
  mounted() {
    this.renderLineChartInUser();
  },
  data() {
    return {
      item: [
        { date: "", productCount: "" },
        { date: "", productCount: "" },
        { date: "", productCount: "" },
        { date: "", productCount: "" },
        { date: "", productCount: "" },
        { date: "", productCount: "" },
        { date: "", productCount: "" },
      ],
    };
  },
  methods: {
    renderLineChartInUser() {
      const ctx = this.$refs.lineChartCanvasInUser.getContext("2d");
      new Chart(ctx, {
        type: "line",
        data: {
          labels: [
            "Thứ hai",
            "Thứ ba",
            "Thứ tư",
            "Thứ năm",
            "Thứ sáu",
            "Thứ bảy",
            "Chủ nhật",
          ],
          datasets: [
            {
              label: "Số người dùng",
              data: [65, 59, 80, 81, 56, 55, 40],
              fill: false,
              borderColor: "rgb(75, 192, 192)",
              tension: 0.1,
            },
          ],
        },
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    },
    async getProductStatisticskInWeek() {
      var res = await this.Get("api/SanPham/GetProductStatisticskInWeek", {});
      if (res.code == "Ok") {
        this.item = res.data;
        this.renderLineChartInItem();
      } else {
        this.$toast.error("Lỗi lấy dữ liệu biểu đồ sản phẩm !", {
          autoClose: 1000,
        });
      }
    },
    renderLineChartInItem() {
      const ctx = this.$refs.lineChartCanvasInItem.getContext("2d");
      new Chart(ctx, {
        type: "line",
        data: {
          labels: [
            "Thứ hai",
            "Thứ ba",
            "Thứ tư",
            "Thứ năm",
            "Thứ sáu",
            "Thứ bảy",
            "Chủ nhật",
          ],
          datasets: [
            {
              label: "Số sản phẩm",
              data: [
                this.item[0].productCount,
                this.item[1].productCount,
                this.item[2].productCount,
                this.item[3].productCount,
                this.item[4].productCount,
                this.item[5].productCount,
                this.item[6].productCount,
              ],
              fill: false,
              borderColor: "rgb(75, 192, 192)",
              tension: 0.1,
            },
          ],
        },
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    },
  },
};
</script>

<style scoped></style>
