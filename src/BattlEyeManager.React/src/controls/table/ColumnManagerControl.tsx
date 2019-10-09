import React, { ReactNode } from "react";
import { Trans } from "react-i18next";
import { Button, Container, Row } from "reactstrap";
import { IBootstrapTableColumn } from "..";

export interface IColumnManagerControlProps<T> {
    columns: Array<IBootstrapTableColumn<T>>,
    children: (props: any) => ReactNode,
}

interface IColumnManagerControlState<T> {
    columns: Array<IBootstrapTableColumn<T>>,
}

export class ColumnManagerControl<T> extends React.Component<IColumnManagerControlProps<T>, IColumnManagerControlState<T>>
{
    constructor(props: IColumnManagerControlProps<T>) {
        super(props);
        const { columns } = this.props;
        this.state = { columns };
    }

    public render() {
        const { children } = this.props;
        const { columns } = this.state;

        const getClassName = (c: IBootstrapTableColumn<T>) => {
            if (c.visible === true) { return "badge badge-pill badge-primary"; }
            return "badge badge-pill badge-secondary";
        }

        const toggleColumn = (c: IBootstrapTableColumn<T>) => {

            // tslint:disable-next-line: no-shadowed-variable
            const { columns } = this.state;
            const ind = columns.indexOf(c);
            const cn = { ...c, visible: !c.visible }
            columns[ind] = cn;

            this.setState({ ...this.state, columns });
        }

        const columnRender = (i: number, column: IBootstrapTableColumn<T>, callback: (c: IBootstrapTableColumn<T>) => void) => {

            const invokator = () => {
                callback(column);
            };
            return <Button key={i} onClick={invokator} className={getClassName(column)}><Trans>{column.header}</Trans></Button>
        }

        return (
            <React.Fragment>
                <Container>
                    <Row>
                        {columns.filter(c => c.hidable === true).map((c, i) => columnRender(i, c, toggleColumn))}
                    </Row>
                    <Row>
                        {children({ ...this.props, columns })}
                    </Row>
                </Container>
            </React.Fragment>
        )
    }
}

