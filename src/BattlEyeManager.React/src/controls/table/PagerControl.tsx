import React, { ReactNode } from "react";
import { Pagination, PaginationItem, PaginationLink } from "reactstrap";


export interface IPagerControlProps<T> {
    data: T[],
    children: (props: any) => ReactNode,
    pageSize: number,
}

interface IPagerControlState {
    page: number
}

export class PagerControl<T> extends React.Component<IPagerControlProps<T>, IPagerControlState>{
    constructor(props: IPagerControlProps<T>) {
        super(props);
        this.state = { page: 1 };

        this.setPage = this.setPage.bind(this);
    }

    public setPage(e: React.MouseEvent<HTMLAnchorElement>, p: number) {
        this.setState({ page: p });
        e.preventDefault();
    }

    public render() {
        const { data, children, pageSize } = this.props;
        let { page } = this.state;

        const total = data.length;
        const maxPage = Math.ceil(total / pageSize);

        if (page < 1) { page = 1; }
        if (page > maxPage) { page = maxPage; }

        const paged = data.slice((page - 1) * pageSize, (page) * pageSize);

        const pagesArray: number[] = [];
        for (let i = page - 1; i > 0 && i > page - 10; i--) {
            pagesArray.push(i);
        }
        pagesArray.reverse();
        pagesArray.push(page);
        for (let i = page + 1; i <= maxPage && i < page + 10; i++) {
            pagesArray.push(i);
        }

        return (
            <React.Fragment>
                {children({ ...this.props, data: paged })}

                <Pagination size="sm" aria-label="">

                    <PaginationItem >
                        {/* tslint:disable-next-line:jsx-no-lambda */}
                        <PaginationLink previous={true} href="#" onClick={e => this.setPage(e, 1)} />
                    </PaginationItem>

                    <PaginationItem >
                        {/* tslint:disable-next-line:jsx-no-lambda */}
                        <PaginationLink previous={true} href="#" onClick={e => this.setPage(e, page - 1)} />
                    </PaginationItem>

                    {pagesArray.map((p, i) =>
                        <PaginationItem key={p} active={p === page}>
                            {/* tslint:disable-next-line:jsx-no-lambda */}
                            <PaginationLink href="#" onClick={e => this.setPage(e, p)}>
                                {p}
                            </PaginationLink>
                        </PaginationItem>)}
                    <PaginationItem>
                        {/* tslint:disable-next-line:jsx-no-lambda */}
                        <PaginationLink next={true} href="#" onClick={e => this.setPage(e, page + 1)} />
                    </PaginationItem>
                    <PaginationItem>
                        {/* tslint:disable-next-line:jsx-no-lambda */}
                        <PaginationLink next={true} href="#" onClick={e => this.setPage(e, maxPage)} />
                    </PaginationItem>
                </Pagination>
                {'Pages: ' + maxPage}

            </React.Fragment>
        )
    }
}


