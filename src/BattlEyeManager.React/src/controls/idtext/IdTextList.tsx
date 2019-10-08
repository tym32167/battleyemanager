import React, { Component } from 'react';
import { Trans } from 'react-i18next';
import { Link } from 'react-router-dom';
import { Table } from 'reactstrap';
import { Button } from "reactstrap";
import { IIdTextItem, IKickReason } from 'src/models';
import { Error } from '../../controls';

interface IListProps<T> {
    items: T[],
    error: any,
    title: string,
    createUrl: string,
    editUrl: string,
    deleteItem: (item: T) => void,
    onLoad: () => void
}

export class IdTextList extends Component<IListProps<IIdTextItem>>{

    constructor(props: IListProps<IIdTextItem>) {
        super(props);

        this.deleteCallback = this.deleteCallback.bind(this);
    }

    public componentDidMount() {
        this.props.onLoad();
    }
    public deleteCallback(item: IIdTextItem) {
        if (window.confirm('Are you sure you want to delete ' + item.text + '?')) {
            const { deleteItem } = this.props;
            deleteItem(item);
        }
    }

    public render() {
        const { items, error, title, createUrl, editUrl } = this.props;
        const len = items.length;

        return (
            <React.Fragment>
                <h2><Trans>{title}</Trans>: ({len})</h2>
                <Error error={error} />
                <Button tag={Link} to={createUrl} color="primary"><Trans>Create</Trans></Button>
                {items && <IdTextTable items={items} deleteItem={this.deleteCallback} editUrl={editUrl} />}
            </React.Fragment>
        );
    }
}

interface IItemsTableProps<T> {
    items: T[],

    editUrl: string,
    deleteItem: (item: T) => void
}

const IdTextTable = ({ items, deleteItem, editUrl }: IItemsTableProps<IIdTextItem>) =>
    <Table size="sm" responsive={true}>
        <thead>
            <tr>
                <th style={{ width: '1%' }} ><Trans>Id</Trans></th>
                <th><Trans>Text</Trans></th>
                <th colSpan={2} className="table-fit" style={{ width: '1%' }} />
            </tr>
        </thead>
        <tbody>
            {items.map((item: IIdTextItem, _: any) => <IdTextItem key={item.id} item={item} deleteItem={deleteItem} editUrl={editUrl} />)}
        </tbody>
    </Table>;

interface IItemProps<T> {
    item: T,
    editUrl: string,
    deleteItem: (item: T) => void
}

const IdTextItem = ({ item, deleteItem, editUrl }: IItemProps<IKickReason>) => {
    const click = (e: any) => deleteItem(item);
    return (
        <tr>
            <td>{item.id}</td>
            <td>{item.text}</td>
            <td>
                <Button color="success" to={editUrl + item.id} tag={Link} size="sm"><Trans>Edit</Trans></Button>
            </td>
            <td>
                <Button color="danger" size="sm" onClick={click}><Trans>Delete</Trans></Button>
            </td>
        </tr>);
}
