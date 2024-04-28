# Sử dụng một image Node.js đã được thiết lập
FROM node:20

# Thiết lập thư mục làm việc trong container
WORKDIR /app

# Sao chép package.json và package-lock.json nếu có và cài đặt các gói
COPY package*.json ./
RUN npm install

# Sao chép mã nguồn ứng dụng
COPY . .

# Mở cổng mà ứng dụng chạy trên
EXPOSE 3000

# Lệnh để khởi chạy ứng dụng
CMD ["npm", "run", "dev"]
