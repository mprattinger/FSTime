export const FormatHours = (value: number): string => {
  const hour = Math.floor(value);
  const minutes = Math.round((value - hour) * 60);

  return `${hour.toString().padStart(2, "0")}:${minutes
    .toString()
    .padStart(2, "0")}`;
};
