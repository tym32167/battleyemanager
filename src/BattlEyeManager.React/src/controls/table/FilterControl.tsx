import React, { ReactNode } from "react";
import { Button, Form, FormGroup, Input } from "reactstrap";


export interface IFilterControlProps<T> {
    data: T[],
    children: (props: any) => ReactNode,
    filter?: (row: T, filterString: string) => boolean,
}

interface IFilterControlState {
    filterString: string
}

export class FilterControl<T> extends React.Component<IFilterControlProps<T>, IFilterControlState>{
    constructor(props: IFilterControlProps<T>) {
        super(props);
        this.state = { filterString: '' };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public handleChange(event: React.FormEvent<HTMLInputElement>) {
        this.state = { filterString: event.currentTarget.value };
    }

    public handleSubmit(event: any) {
        this.setState(this.state);
        event.preventDefault();
    }

    public defaultFilter(row: T, filterString: string) {
        const lower = filterString.toLowerCase();
        for (const key in row) {
            if (String(row[key]).toLowerCase().includes(lower)) {
                return true;
            }
        }
        return false;
    }

    public render() {
        const { data, children, filter } = this.props;
        const { filterString } = this.state;

        let filtered = data;

        if (filter === undefined) {
            filtered = data.filter((el, ind, arr) => this.defaultFilter(el, filterString));
        }
        else {
            filtered = data.filter((el, ind, arr) => filter(el, filterString));
        }



        return (
            <React.Fragment>
                <Form inline={true} onSubmit={this.handleSubmit}>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="text" name="filter" id="filter" placeholder="filter..."
                            onChange={this.handleChange} />
                    </FormGroup>
                    <Button>Filter</Button>
                </Form>
                {children({ data: filtered })}
            </React.Fragment>
        )
    }
}


