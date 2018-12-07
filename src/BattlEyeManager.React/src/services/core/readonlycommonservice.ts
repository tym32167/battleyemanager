import axios from 'axios';
export class ReadonlyCommonService<T>  {
    constructor(readonly baseUrl: string = '') { }
    public getItems() {
        return axios.get<T[]>(this.baseUrl)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }

    public getItemsBy(baseUrl: string) {
        return axios.get<T[]>(baseUrl)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }

    public getItem(id: string | number) {
        return axios.get<T>(this.baseUrl + id)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }

    public getError(error: any) {
        if (error.response && error.response.data) { return error.response.data; }
        return error.message;
    }
}