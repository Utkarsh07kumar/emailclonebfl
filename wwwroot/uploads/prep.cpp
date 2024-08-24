#include<bits/stdc++.h>
using namespace std;
// 1 remove adj duplicate
// int main(){
//     string str="abbacad";
//     int n=str.length();
//     string ans="";
//     int i=0;
//     while(i<n)
//     {
//         if(ans.length()>0){
//             if(ans[ans.length()-1]==str[i]){
//                 ans.pop_back();
//             }
//             else{
//                 ans.push_back(str[i]);
//             }
//         }
//         else{
//             ans.push_back(str[i]);
//         }
//         i++;
//     }
//     cout<<ans;
//     return 0;
// }
// remove all duplicate 
// #include<bits/stdc++.h>
// using namespace std;
// int main(){
//     string str="abacdeef";
//     string ans="";
//     int i=0;
//     for(int i=0;i<str.length();i++)
//     {
//         bool flag=false;
//         for(int j=0;j<ans.length();j++)
//         {
//             if(ans[j]==str[i]){
//                 flag=true;
//                 break;
//             }
//         }
//         if(!flag)
//         ans.push_back(str[i]);
//     }
//     cout<<ans;
// }
// 3fibonaci series
// int main(){
//     int t1=0,t2=1,nextterm;
//     int n;
//     cin>>n;
//     for(int i=0;i<n;i++){
//         if(i<=1) nextterm=i;
//         else{
//             nextterm=t1+t2;
//             t1=t2;
//             t2=nextterm;
//         }
//         cout<<nextterm<<" ";
//     }
// }
// recursive
// int fib(int n){
//     if(n<=1) return n;
//     return fib(n-1)+fib(n-2);
// }
// void printfib(int n){
//     for(int i=0;i<n;i++){
//         cout<<fib(i)<<" ";
//     }
// }
// int main(){
//     int n;
//     cin>>n;
//     printfib(n);
//     return 0;
// }
// most freq character
// int main(){
//     string str="aaabdeefdfd";
//     int hash[26]={0};
//     for(int i=0;i<str.length();i++){
//         hash[str[i]-'a']++;
//     }
//     char most_freq_char;
//     int most_freq=INT_MIN;
//     for(int i=0;i<26;i++){
//         if(hash[i]>most_freq){
//             most_freq=hash[i];
//             most_freq_char=i+'a';
//         }
//     }
//     cout<<most_freq_char;
//     return 0;
// }
// prime number
// int main(){
//     int n;
//     cin>>n;
//     for(int i=2;i<=n;i++){
//         bool isprime=true;
//         for(int j=2;j<=i/2;j++){
//             if(i%j==0){
//                 isprime=false;
//                 break;
//             }
//         }
//         if(isprime)
//         cout<<i<<" ";
//     }
//     return 0;
// }
// bool isprime(int n){
//     for(int i=2;i<=n/2;i++){
//         if(n%i==0) return false;
//     }return true;
// }
// int main(){
//     int n;
//     cin>>n;
//     for(int i=2;i<=n;i++){
//         if(isprime(i)) cout<<i<<" ";
//     }
// }

// factorial
// int main(){
//     int n;
//     cin>>n;
//     int ans=1;
//     for(int i=1;i<=n;i++){
//         ans*=i;
//     }
//     cout<<ans;
//     return 0;
// }
// Write a Program to Remove the Vowels from a String
// bool isvowel(char ch){
//     if(ch=='a' || ch=='e' || ch== 'i' || ch=='o' || ch=='u') return true;
//     else return false;
// }
// int main(){
//     string str="geeksforgeek";
//     string ans="";
//     for(int i=0;i<str.length();i++){
//         if(!isvowel(str[i])){
//             ans.push_back(str[i]);
//         }
//     }
//     cout<<ans;
//     return 0;
// }
// palindrome
// int main(){
//     int n=121;
//     int on=n;
//     int rn=0;
//     while(n!=0){
//         int rem=n%10;
//         rn=rn*10+rem;
//         n/=10;
//     } 
//     if(rn==on) cout<<"true";
//     else cout<<"false";
//     return 0;
// }
// gcd or hcf of two number
// int main(){
//     int a=54,b=33;
//     int ans=min(a,b);
//     while(ans>0){
//         if(a%ans==0 && b%ans==0) break;
//         ans--;
//     }
//     cout<<ans;
//     return 0;
// }
// lcm
int gcd(int a,int b){
    int ans=min(a,b);
    while(ans>0){
        if(a%ans==0 && b%ans==0) break;
        ans--;
    }return ans;
}
int main(){
    int a=10,b=5;
    int ans=a*b;
    int finalans=ans/gcd(a,b);
    cout<<finalans;
}