import React from 'react';
import { Table } from 'reactstrap';


export interface IBootstrapTableColumn<T> {
    header: string,
    name?: string,

    headerStyle?: any,
    rowStyle?: any,
    renderer?: (row: T) => any
}

export interface IBootstrapTableProps<T> {
    columns: Array<IBootstrapTableColumn<T>>,
    data: T[],

    handleSort?: (name: string) => void
}

interface IBootstrapItemProps<T> {
    columns: Array<IBootstrapTableColumn<T>>,
    item: T
}

export const BootstrapTable = <T extends any>(props: IBootstrapTableProps<T>) => {
    const { data, columns, handleSort } = props;

    return (
        <Table size="sm" responsive={true}>
            <thead>
                <tr>
                    {columns && columns.map((item, i) => BootstrapColumn(i, item, handleSort))}
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
            {columns && columns.map((column, i) => <td key={i} style={{ ...column.rowStyle }}>{rowRrenderer(column, item)}</td>)}
        </tr>
    );
}

const rowRrenderer = <T extends any>(column: IBootstrapTableColumn<T>, row: T) => {
    let renderer = column.renderer;
    if (renderer === undefined) {
        if (column.name !== undefined) {
            const name = column.name;
            renderer = (r: T) => r[name];
        }
    }

    if (renderer === undefined) {
        renderer = (r: T) => "Nothing to render";
    }

    return renderer(row);
}

const BootstrapColumn = <T extends any>(key: number, props: IBootstrapTableColumn<T>, handleSort: ((name: string) => void) | any) => {
    const { header, headerStyle, name } = props;

    const invokator = () => {
        if (handleSort !== undefined) {
            handleSort(name);
        }
    }

    return (
        <th key={key} onClick={invokator} style={{ ...headerStyle }}>{header}</th>
    );
}