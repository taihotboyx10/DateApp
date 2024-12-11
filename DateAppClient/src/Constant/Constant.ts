export const API_URLS = {
  BASE_URL: "https://localhost:5097/api",
  USER: {
    LOGIN: "/account/login",
    GET_ALL_USER: "/user",
    REGISTER: "/account/register",
    GET_USER_BY_ID: (id: number) => `/user/${id}`,
  },     
}
