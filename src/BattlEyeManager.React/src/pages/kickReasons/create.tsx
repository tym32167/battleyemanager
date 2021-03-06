import { connect } from "react-redux";
import { Action, Dispatch } from 'redux';
import { IdTextEdit } from "src/controls";
import { IKickReason } from 'src/models';
import { kickReasonActions } from "../../store/actions";

const mapStateToProps = ({ kickReasons }: { kickReasons: any }) => {
    return {
        error: kickReasons.error,
        form: "kick_reasaon_create",
        listUrl: '/kickReasons',
        title: "Create kick reason",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>) => {
    return {
        onSubmit: (item: IKickReason) => kickReasonActions.addItem(item)(dispatch)
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextEdit);

export { ConnectedCreate as Create };