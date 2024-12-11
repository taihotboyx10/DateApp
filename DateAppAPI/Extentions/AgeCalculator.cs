namespace DateAppAPI.Extentions;

public static class AgeCalculator{
    public static int CalcAge(this DateOnly doB){
        var today = DateOnly.FromDateTime(DateTime.Now);
        int age = today.Year - doB.Year;
        // nếu chưa đến ngày sinh nhật thì trừ đi 1 tuổi
        if(today.AddYears(-age) < doB){
            age--;
        }

        return age;
    }
}