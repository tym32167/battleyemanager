import { IService } from "src/services";
import { IIdentity } from "../../../models";
import { history } from '../../../services';
import { SubjectConstants } from "../../constants";
import { CommonActions } from "../commonActions";
import { ReadonlyActionBase } from "./readonlyactionsbase";

export class ActionBase<T extends IIdentity> extends ReadonlyActionBase<T>
{
    constructor(
        readonly commonActions: CommonActions,
        readonly subject: SubjectConstants,
        readonly service: IService<T>
    ) {
        super(commonActions, subject, service)
    }

    public deleteItem(item: T) {
        return this.commonActions.deleteItem<T>(item, this.subject, this.service, (_: any, dispatch: any) => {
            dispatch(this.getItems());
        });
    }

    public addItem(item: T) {
        return this.commonActions.addItem<T>(item, this.subject, this.service, () => history.push('/users'));
    }

    public updateItem(item: T) {
        return this.commonActions.updateItem<T>(item, this.subject, this.service, () => history.push('/users'));
    }
}

