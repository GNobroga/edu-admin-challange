export const ApiConfig = {
  baseUrl: 'http://localhost:5276',
  baseUrlWith(path: string) {
    return `${this.baseUrl}/${path}`;
  }
};
