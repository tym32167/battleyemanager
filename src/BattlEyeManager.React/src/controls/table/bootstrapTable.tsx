import React from 'react';
import { Table } from 'reactstrap';


export interface IBootstrapTableColumn<T> {
    header: string,

    headerStyle?: any,
    rowStyle?: any,
    renderer: (row: T) => any
}

export interface IBootstrapTableProps<T> {
    columns: Array<IBootstrapTableColumn<T>>,
    data: T[]
}

interface IBootstrapItemProps<T> {
    columns: Array<IBootstrapTableColumn<T>>,
    item: T
}

export const BootstrapTable = <T extends any>(props: IBootstrapTableProps<T>) => {
    const { data, columns } = props;
    // tslint:disable-next-line:no-console
    console.log(data);
    return (
        <Table size="sm" responsive={true}>
            <thead>
                <tr>
                    {columns && columns.map((item, i) => <th key={i} style={{ ...item.headerStyle }} >{item.header}</th>)}
                </tr>
            </thead>
            <tbody>
                {data && data.map((item, i) => <BootstrapItem key={i} {...{ item, columns }} />)}
            </tbody>
        </Table>
    );
}

const BootstrapItem = <T extends any>(props: IBootstrapItemProps<T>) => {
    const { item, columns } = props;
    return (
        <tr>
            {columns && columns.map((column, i) => <td key={i} style={{ ...column.rowStyle }}>{column.renderer(item)}</td>)}
        </tr>
    );
}
