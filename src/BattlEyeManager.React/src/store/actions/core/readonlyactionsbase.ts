import { IReadonlyService } from "../../../services";
import { CommonActions } from "../commonActions";



export class ReadonlyActionBase<T>
{
    constructor(
        readonly commonActions: CommonActions,
        readonly subject: string,
        readonly service: IReadonlyService<T>
    ) { }

    public getItems() {
        return this.commonActions.getItems<T>(this.subject, this.service);
    }

    public getItem(id: string) {
        return this.commonActions.getItem<T>(id, this.subject, this.service);
    }
}

