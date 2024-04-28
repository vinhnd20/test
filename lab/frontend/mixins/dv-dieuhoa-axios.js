import axios from "axios";
const DVDieuHoaAxios = {
  beforeMount() {
    let name = "dieuhoa-token=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(";");
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == " ") {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        this.token = c.substring(name.length, c.length);
      }
    }
  },
  props: {},

  data() {
    return {
      baseApi: "https://217.15.171.103:51926/",
      token: "",
    };
  },

  methods: {
    async Get(path, parrams) {
      try {
        const headers = {
          Authorization: `Bearer ${this.token}`,
        };

        const response = await axios.get(this.baseApi + path, {
          headers: headers,
          params: parrams,
        });
        return response.data;
      } catch (error) {
        this.$toast.error("Call api error!", {
          autoClose: 1000,
        });
        return;
      }
    },

    async Post(path, bodyRequest) {
      try {
        const headers = {
          Authorization: `Bearer ${this.token}`,
          "Content-Type": "application/json",
        };

        const response = await axios.post(this.baseApi + path, bodyRequest, {
          headers: headers,
        });
        return response.data;
      } catch (error) {
        this.$toast.error("Call api error!", {
          autoClose: 1000,
        });
        return;
      }
    },

    async Put(path, bodyRequest) {
      try {
        const headers = {
          Authorization: `Bearer ${this.token}`,
          "Content-Type": "application/json",
        };

        const response = await axios.put(this.baseApi + path, bodyRequest, {
          headers: headers,
        });
        return response.data;
      } catch (error) {
        this.$toast.error("Call api error!", {
          autoClose: 1000,
        });
        return;
      }
    },
    async Delete(path, params) {
      try {
        const headers = {
          Authorization: `Bearer ${this.token}`,
        };

        const response = await axios.delete(this.baseApi + path, {
          headers: headers,
          params: params,
        });
        return response.data;
      } catch (error) {
        this.$toast.error(`${error}`, {
          autoClose: 1000,
        });
        return;
      }
    },
    async PostFormData(path, bodyRequest) {
      try {
        const headers = {
          Authorization: `Bearer ${this.token}`,
          "Content-Type": "multipart/form-data",
        };

        const response = await axios.post(this.baseApi + path, bodyRequest, {
          headers: headers,
        });
        return response.data;
      } catch (error) {
        this.$toast.error("Call api error!", {
          autoClose: 1000,
        });
        return;
      }
    },

    async PutFormData(path, bodyRequest) {
      try {
        const headers = {
          Authorization: `Bearer ${this.token}`,
          "Content-Type": "multipart/form-data",
        };

        const response = await axios.put(this.baseApi + path, bodyRequest, {
          headers: headers,
        });
        return response.data;
      } catch (error) {
        this.$toast.error("Call api error!", {
          autoClose: 1000,
        });
        return;
      }
    },
  },
};

export default DVDieuHoaAxios;
