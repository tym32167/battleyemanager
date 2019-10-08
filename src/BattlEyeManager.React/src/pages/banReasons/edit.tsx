import React, { Component } from 'react';
import { connect } from "react-redux";
import { Action, Dispatch } from 'redux';
import { InjectedFormProps } from 'redux-form';
import { IdTextEdit } from "src/controls";
import { IBanReason, IIdTextItem } from 'src/models';
import { banReasonActions } from "../../store/actions";

interface IEditProps<T> extends InjectedFormProps {
    item: T,
    error: any,
    title: string,
    form: string,
    listUrl: string,
    onSubmit: (item: IIdTextItem) => void,
    onLoad: () => void
}

class Edit extends Component<IEditProps<IIdTextItem>>{
    public componentDidMount() {
        this.props.onLoad();
    }

    public render() {
        const { item } = this.props;
        return (
            <React.Fragment>
                {item && <IdTextEdit {...this.props} />}
                {!item && "Loading..."}
            </React.Fragment>
        )
    }
}


const mapStateToProps = ({ banReasons }: { banReasons: any }) => {
    return {
        error: banReasons.error,
        form: "ban_reasaon_edit",
        item: banReasons.item,
        listUrl: '/banReasons',
        title: "Edit ban reason",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>,
    { match: { params: { id } } }: { match: { params: { id: string } } }) => {
    return {
        onLoad: () => banReasonActions.getItem(id)(dispatch),
        onSubmit: (item: IBanReason) => banReasonActions.updateItem(item)(dispatch)
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export { ConnectedCreate as Edit };