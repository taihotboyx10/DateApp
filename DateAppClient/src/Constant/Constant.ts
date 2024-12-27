export const API_URLS = {
  BASE_URL: "https://localhost:5097/api",
  USER: {
    LOGIN: "/account/login",
    REGISTER: "/account/register",
    GET_ALL_USER: "/user",
    GET_USER_BY_ID: (id: number) => `/user/user-id/${id}`,
    GET_USER_BY_NAME: (userName: string) => `/user/user-name/${userName}`,
    DELETE_USER: (id: number) => `/account/delete/${id}`,
    UPDATE_USER: (id: number) => `/account/update/${id}`,
  },     
}
