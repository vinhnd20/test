import CryptoJS from "crypto-js";

const Utility = {
  data() {
    return {};
  },
  methods: {
    // getCookies() {
    //     var pairs = document.cookie.split(";");
    //     var cookies = {};
    //     for (var i = 0; i < pairs.length; i++) {
    //         var pair = pairs[i].split("=");
    //         cookies[(pair[0] + '').trim()] = unescape(pair.slice(1).join('='));
    //     }
    //     return cookies;
    // },

    deleteAllLocalStorages() {
      localStorage.clear();
    },

    deleteAllCookies() {
      const cookies = document.cookie.split(";");

      for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i];
        const eqPos = cookie.indexOf("=");
        const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
      }
    },

    formatDate(stringDate) {
      var inputDate = new Date(stringDate);
      const daysOfWeek = [
        "Chủ Nhật",
        "Thứ Hai",
        "Thứ Ba",
        "Thứ Tư",
        "Thứ Năm",
        "Thứ Sáu",
        "Thứ Bảy",
      ];

      const addLeadingZero = (number) => {
        return number < 10 ? "0" + number : number;
      };

      const dayOfWeek = daysOfWeek[inputDate.getDay()];
      const day = addLeadingZero(inputDate.getDate());
      const month = addLeadingZero(inputDate.getMonth() + 1); // Tháng bắt đầu từ 0
      const year = inputDate.getFullYear();
      const formattedDate = `${dayOfWeek}, ${day} Tháng ${month}, ${year}`;
      return formattedDate;
    },

    encryptedPage(information) {
      var encryptedAES = CryptoJS.AES.encrypt(information, "kjdshfh");
      return encryptedAES;
    },
    decryptedPage(encode) {
      var decryptedAES = CryptoJS.AES.decrypt(encode, "kjdshfh");
      return (decryptedAES + "").at(-1);
    },
    formatImage(img) {
      const config = useRuntimeConfig();
      if (img == "") {
        return config.public.BaseUrl + "default/avatar-trang.jpg";
      }
      return config.public.BaseUrl + img;
    },
    convertUrlToFile(path) {
      const urlImg = this.formatImage(path);
      const file = new File([urlImg], urlImg, { type: "image/png" });
      console.log(file);
      const obj = {
        name: path,
        type: "image/*",
        ext: "*",
        file: file,
        url: urlImg,
      };
      return obj;
    },
  },
};

export default Utility;
