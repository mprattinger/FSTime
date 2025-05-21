import { PropsWithChildren } from "react";

interface IProps extends PropsWithChildren {
  label: string;
}

export const TextWithLabel = (props: IProps) => {
  return (
    <div>
      <div className="text-sm text-gray-500">{props.label}</div>
      <div>{props.children}</div>
    </div>
  );
};
