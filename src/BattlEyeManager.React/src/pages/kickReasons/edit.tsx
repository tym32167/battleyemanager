import React, { Component } from 'react';
import { connect } from "react-redux";
import { Action, Dispatch } from 'redux';
import { InjectedFormProps } from 'redux-form';
import { IdTextEdit } from "src/controls";
import { IIdTextItem, IKickReason } from 'src/models';
import { kickReasonActions } from "../../store/actions";

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


const mapStateToProps = ({ kickReasons }: { kickReasons: any }) => {
    return {
        error: kickReasons.error,
        form: "kick_reasaon_edit",
        item: kickReasons.item,
        listUrl: '/kickReasons',
        title: "Edit kick reason",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>,
    { match: { params: { id } } }: { match: { params: { id: string } } }) => {
    return {
        onLoad: () => kickReasonActions.getItem(id)(dispatch),
        onSubmit: (item: IKickReason) => kickReasonActions.updateItem(item)(dispatch)
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export { ConnectedCreate as Edit };