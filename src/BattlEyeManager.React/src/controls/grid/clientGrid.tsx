import React from "react";
import { Trans } from "react-i18next";
import { BootstrapTable, Error, FilterControl, IFilterControlProps, IPagerControlProps, ISortControlProps, PagerControl, SortControl } from "../index";


export interface IClientGridColumn<T> {
    header: string,
    className?: string,
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
    showLen?: boolean,
    enableFilter?: boolean,
    enableSort?: boolean,
    enablePager?: boolean,
    data?: T[],
    error?: any,
}

export interface IClientGridState<T> {
    columns?: Array<IClientGridColumn<T>>,
    header?: (props: any) => React.ReactNode;
}

export class ClientGridColumn<T> extends React.Component<IClientGridColumn<T>>{
}

// tslint:disable-next-line: max-classes-per-file
export class ClientGridColumns<T> extends React.Component<any>{
}

// tslint:disable-next-line: max-classes-per-file
export class ClientGridHeader<T> extends React.Component<any>{
}

// tslint:disable-next-line: max-classes-per-file
export class ClientGrid<T> extends React.Component<IClientGridProps<T>, IClientGridState<T>>{

    constructor(props: IClientGridProps<T>) {
        super(props);

        const columns = Array<IClientGridColumn<T>>();

        React.Children.toArray(this.props.children)
            .map(v => (v as unknown as ClientGridColumns<T>))
            .filter(v => v)
            .forEach(v => {
                React.Children.toArray(v.props.children)
                    .map(c => (c as unknown as ClientGridColumn<T>))
                    .filter(c => c)
                    .map(c => c.props)
                    .forEach(c => columns.push(c));
            });

        this.state = { columns };
    }

    public render() {
        const { columns } = this.state;
        const { data, error, header, showLen, beforeGrid, enableFilter, enableSort, enablePager } = this.props;

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

        let lenHeader = "(" + len + ")";
        if (showLen === false) {
            lenHeader = "";
        }

        return (
            <React.Fragment>
                <Error error={error} />

                <h2>{<Trans>{header}</Trans>}  {lenHeader}</h2>
                {beforeGrid && beforeGrid()}

                {data &&
                    <React.Fragment>
                        {renderer(tableProps)}
                    </React.Fragment>}
            </React.Fragment>);
    }
}

