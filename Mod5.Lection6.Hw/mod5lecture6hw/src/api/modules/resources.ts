import apiClient from "../client";

export const getById = (id: string) => apiClient({
  path: `unknown/${id}`,
  method: 'GET'
})

export const getByPage = (page: number) => apiClient({
  path: `unknown?page=${page}`,
  method: 'GET'
})