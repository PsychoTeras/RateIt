package com.RateIt.entity;

/**
 * Created by igor on 7/16/14.
 */
public class BaseQueryResult {
    public int getErrorCode() {
        return ErrorCode;
    }

    public void setErrorCode(int errorCode) {
        ErrorCode = errorCode;
    }

    public String getErrorMessage() {
        return ErrorMessage;
    }

    public void setErrorMessage(String errorMessage) {
        ErrorMessage = errorMessage;
    }

    public int ErrorCode;
    public String ErrorMessage;
}
