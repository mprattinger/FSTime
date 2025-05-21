import { PropsWithChildren } from "react";

export const Card = (props: PropsWithChildren) => {
  return (
    <div className="bg-white shadow-xl p-4 rounded-xl">{props.children}</div>
  );
};
