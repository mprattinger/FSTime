import { FormatHours } from "../../../common/utils/Formatters";
import { Workschedule } from "../../employee/services/EmployeeService";

interface IProps {
  schedule: Workschedule;
}

export const WorkscheduleInfo = (props: IProps) => {
  return (
    <div>
      <div className="text-sm text-gray-500">Wochenstunden</div>
      <div className="flex gap-x-4">
        <div>
          <div className="text-sm ">Mo</div>
          <div>{FormatHours(props.schedule.monday)}</div>
        </div>
        <div>
          <div className="text-sm">Di</div>
          <div>{FormatHours(props.schedule.tuesday)}</div>
        </div>
        <div>
          <div className="text-sm">Mi</div>
          <div>{FormatHours(props.schedule.wednesday)}</div>
        </div>
        <div>
          <div className="text-sm">Do</div>
          <div>{FormatHours(props.schedule.thursday)}</div>
        </div>
        <div>
          <div className="text-sm">Fr</div>
          <div>{FormatHours(props.schedule.friday)}</div>
        </div>
        {props.schedule.saturday !== 0 && (
          <div>
            <div className="text-sm">Sa</div>
            <div>{FormatHours(props.schedule.saturday)}</div>
          </div>
        )}
        {props.schedule.sunday !== 0 && (
          <div>
            <div className="text-sm">So</div>
            <div>{FormatHours(props.schedule.sunday)}</div>
          </div>
        )}
      </div>
    </div>
  );
};
