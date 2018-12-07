import { connect } from "react-redux";
import { Dispatch } from 'redux';
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

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onSubmit: (item: IKickReason) => dispatch(kickReasonActions.addItem(item))
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextEdit);

export { ConnectedCreate as Create };