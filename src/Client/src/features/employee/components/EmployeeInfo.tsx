import { useEffect } from "react";
import { useAuth } from "../../auth/hooks/useAuth";
import { useSecureApi } from "../../auth/hooks/useSecureApi";
import { GetMyEmployeeInfo } from "../services/EmployeeService";
import { ApplicationError } from "../../../common/Types";
import { Card } from "../../../common/components/Card";
import { TextWithLabel } from "../../../common/components/TextWithLabel";
import { WorkscheduleInfo } from "../../workschedules/components/WorkscheduleInfo";

export const EmployeeInfo = () => {
  const { employee, setEmployee } = useAuth();
  const api = useSecureApi();

  useEffect(() => {
    const reloadEmployeeData = async () => {
      const data = await GetMyEmployeeInfo(api);
      if (data) {
        if (data instanceof ApplicationError) {
          //If not ok, show error message
          return;
        }
        setEmployee(data);
      }
    };

    if (!employee) {
      reloadEmployeeData();
    }
  }, [employee, setEmployee, api]);

  return (
    <div className="flex flex-col m-4 gap-y-4">
      <div className="flex gap-x-4">
        <Card>
          <div className="flex flex-col gap-y-2">
            <TextWithLabel label="Mitarbeiter">
              {employee?.firstName} {employee?.lastName}
            </TextWithLabel>
            <TextWithLabel label="Vorgesetzter">Wilfried Lautner</TextWithLabel>
            <TextWithLabel label="Im Unternehmen seit">
              01.01.2003
            </TextWithLabel>
            {employee?.workschedules
              .filter((x) => x.to === null)
              .map((x) => (
                <WorkscheduleInfo schedule={x} />
              ))}
          </div>
        </Card>
      </div>
      <div>
        <Card>Zeiterfassung</Card>
      </div>
    </div>
  );
};
