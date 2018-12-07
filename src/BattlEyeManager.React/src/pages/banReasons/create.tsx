import { connect } from "react-redux";
import { Dispatch } from 'redux';
import { IdTextEdit } from "src/controls";
import { IBanReason } from 'src/models';
import { banReasonActions } from "../../store/actions";

const mapStateToProps = ({ banReasons }: { banReasons: any }) => {
    return {
        error: banReasons.error,
        form: "ban_reasaon_create",
        listUrl: '/banReasons',
        title: "Create ban reason",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onSubmit: (item: IBanReason) => dispatch(banReasonActions.addItem(item))
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextEdit);

export { ConnectedCreate as Create };