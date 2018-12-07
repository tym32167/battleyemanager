import React, { ReactNode } from "react";

export interface ISortControlProps<T> {
    data: T[],
    children: (props: any) => ReactNode
}

interface ISortControlState {
    sortField?: string,
    sortDirection?: boolean, // true - ask, false - desk, undef - none
}

export class SortControl<T> extends React.Component<ISortControlProps<T>, ISortControlState>{
    constructor(props: ISortControlProps<T>) {
        super(props);
        this.state = {};

        this.handleSort = this.handleSort.bind(this);
    }

    public handleSort(fieldName: string) {
        if (fieldName !== undefined) {

            const { sortField, sortDirection } = this.state;

            if (sortField === fieldName) {
                this.setState({ sortField: fieldName, sortDirection: !sortDirection });
            }
            else {
                this.setState({ sortField: fieldName, sortDirection: true });
            }
        }
    }

    public render() {
        const { data, children } = this.props;
        const { sortField, sortDirection } = this.state;

        let sorted = data;

        if (sortField !== undefined) {
            if (sortDirection === true) {
                sorted = sorted.sort((a, b) => a[sortField] > b[sortField] ? 1 : (a[sortField] < b[sortField] ? -1 : 0));
            }

            if (sortDirection === false) {
                sorted = sorted.sort((a, b) => a[sortField] > b[sortField] ? -1 : (a[sortField] < b[sortField] ? 1 : 0));
            }
        }

        return (
            <React.Fragment>
                {children({ ...this.props, data: sorted, handleSort: this.handleSort })}
            </React.Fragment>
        )
    }
}


