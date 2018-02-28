import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'serverlist',
    templateUrl: './serverlist.component.html'
})
export class ServerListComponent {
    public servers: ServerInfo[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/server').subscribe(result => {
            this.servers = result.json() as ServerInfo[];
        }, error => console.error(error));
    }
}

interface ServerInfo {
    id: number;
    name: string;
    port: number;
}
