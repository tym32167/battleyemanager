import axios from 'axios';
import { IIdentity } from 'src/models';
import { ReadonlyCommonService } from './readonlycommonservice';

export class CommonService<T extends IIdentity> extends ReadonlyCommonService<T>  {
    constructor(readonly baseUrl: string = '') {
        super(baseUrl);
    }

    public updateItem(item: T) {
        return axios.post<T>(this.baseUrl + item.id, item)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }

    public addItem(item: T) {
        return axios.put<T>(this.baseUrl, item)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }

    public addItemBy(baseUrl: string, item: T) {
        return axios.put<T>(baseUrl, item)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }

    public deleteItem(id: string | number) {
        return axios.delete(this.baseUrl + id)
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }
}