import React, { ReactNode } from "react";


export interface ISortProps<T> {
    sortField?: string,
    sortDirection?: boolean,
}

export interface ISortControlProps<T> extends ISortProps<T> {
    data: T[],
    sortField?: string,
    sortDirection?: boolean,
    children: (props: any) => ReactNode
}



interface ISortControlState {
    sortField?: string,
    sortDirection?: boolean, // true - ask, false - desk, undef - none
}

export class SortControl<T> extends React.Component<ISortControlProps<T>, ISortControlState>{
    constructor(props: ISortControlProps<T>) {
        super(props);

        const { sortField, sortDirection } = this.props;
        this.state = { sortField, sortDirection };

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
                sorted = sorted.sort((a, b) => this.compare(a[sortField], b[sortField]));
            }

            if (sortDirection === false) {
                sorted = sorted.sort((a, b) => - this.compare(a[sortField], b[sortField]));
            }
        }

        return (
            <React.Fragment>
                {children({ ...this.props, data: sorted, handleSort: this.handleSort })}
            </React.Fragment>
        )
    }

    private compare(left: any, right: any) {
        if (typeof (left) === "string" && typeof (right) === "string") {
            return this.compareString(left, right);
        }

        if (left > right) { return 1; }
        if (left < right) { return -1; }
        return 0;
    }

    private compareString(left: string, right: string) {
        const l1 = left.toLocaleLowerCase();
        const l2 = right.toLocaleLowerCase();
        return l1.localeCompare(l2);
    }
}


