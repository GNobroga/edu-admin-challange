export const ApiConfig = {
  baseUrl: 'http://localhost:8080',
  baseUrlWith(path: string) {
    return `${this.baseUrl}/${path}`;
  }
};
