import React from "react";
import { BootstrapTable, Error, FilterControl, IFilterControlProps, IPagerControlProps, ISortControlProps, PagerControl, SortControl } from "../index";


export interface IClientGridColumn<T> {
    header: string,
    name: string,
    headerStyle?: any,
    rowStyle?: any,
    renderer?: (row: T) => any
}

export interface IGridParentProps<T> {
    data?: T[],
    error?: any,
}

export interface IClientGridProps<T> {
    beforeGrid?: () => any,
    header: string,
    enableFilter?: boolean,
    enableSort?: boolean,
    enablePager?: boolean,
    data?: T[],
    error?: any,
}

export interface IClientGridState<T> {
    columns?: Array<IClientGridColumn<T>>,
}

export class ClientGridColumn<T> extends React.Component<IClientGridColumn<T>>{
}

// tslint:disable-next-line: max-classes-per-file
export class ClientGrid<T> extends React.Component<IClientGridProps<T>, IClientGridState<T>>{

    constructor(props: IClientGridProps<T>) {
        super(props);

        const columns = React.Children.map(this.props.children, (child) => {
            return (child as unknown as ClientGridColumn<T>).props;
        })

        this.state = { columns };
    }

    public render() {
        const { columns } = this.state;
        const { data, error, header, beforeGrid, enableFilter, enableSort, enablePager } = this.props;

        const len = data ? data.length : 0;

        let renderer = (p: any) => <BootstrapTable columns={columns} {...p} />;

        if (enablePager === true) {
            const prev = renderer;
            renderer = (props: any) => {
                const pagerProps: IPagerControlProps<T> = {
                    ...props,
                    children: prev,
                    pageSize: 50,
                }
                return (<PagerControl {...pagerProps} />);
            };
        }

        if (enableFilter === true) {
            const prev = renderer;
            renderer = (props: any) => {
                const filterProps: IFilterControlProps<T> = {
                    ...props,
                    children: prev
                };
                return (<FilterControl {...filterProps} />);
            };
        }

        if (enableSort === true) {
            const prev = renderer;
            renderer = (props: any) => {
                const sortProps: ISortControlProps<T> = {
                    children: prev,
                    ...props
                };
                return (<SortControl {...sortProps} />);
            };
        }

        const tableProps = {
            data: data || []
        };

        return (
            <React.Fragment>
                <h2>{header} ({len})</h2>
                <Error error={error} />
                {beforeGrid && beforeGrid()}

                {data &&
                    <React.Fragment>
                        {renderer(tableProps)}
                    </React.Fragment>}
            </React.Fragment>);
    }
}

