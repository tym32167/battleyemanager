import React from "react";
import { BootstrapTable, Error, FilterControl, IFilterControlProps, IPagerControlProps, ISortControlProps, PagerControl, SortControl } from "../index";


export interface IClientGridColumn<T> {
    header: string,
    name: string,
    headerStyle?: any,
    rowStyle?: any,
    renderer?: (row: T) => any
}

export interface IClientGridProps<T> {
    fetch: () => Promise<T[]>,
    beforeGrid?: () => any,
    header: string,

    enableFilter?: boolean,
    enableSort?: boolean,
    enablePager?: boolean,
}

export interface IClientGridState<T> {
    data?: T[],
    error?: any,
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

    public async componentDidMount() {
        const callb = this.props.fetch;
        if (callb) {
            callb().then(
                (items: T[]) => this.setState({ data: items, error: undefined, ... this.state }),
                (error: any) => this.setState({ data: undefined, error, ... this.state }));
        }

    }

    public render() {
        const { data, error, columns } = this.state;
        const len = data ? data.length : 0;

        const { header, beforeGrid, enableFilter, enableSort, enablePager } = this.props;


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

