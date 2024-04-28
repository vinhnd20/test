<template>
  <div class="mt-3 px-3">
    <!-- thống kê -->
    <Statistic></Statistic>
    <!-- xử lý quản lý điều hoà -->
    <div>
      <!-- Biểu đồ thống kê -->
      <Chart></Chart>
    </div>
  </div>
</template>
<script>
import Chart from "~/components/Chart.vue";
import Statistic from "~/components/Statistic.vue";

definePageMeta({
  layout: "default",
});
</script>

<style scoped></style>
